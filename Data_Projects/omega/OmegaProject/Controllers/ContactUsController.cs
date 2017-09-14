using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OmegaProject.Models;
using Microsoft.AspNetCore.Authorization;
using MimeKit;
using MailKit.Net.Smtp;

namespace OmegaProject.Controllers
{
    public class ContactUsController : Controller
    {
        private readonly OmegaProjectContext _context;

        public ContactUsController(OmegaProjectContext context)
        {
            _context = context;    
        }

        // GET: ContactUs
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.ContactUs.OrderByDescending(c => c.ConId).ToListAsync());
        }

        // GET: ContactUs/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactUs = await _context.ContactUs
                .SingleOrDefaultAsync(m => m.ConId == id);
            if (contactUs == null)
            {
                return NotFound();
            }

            return View(contactUs);
        }

        // GET: ContactUs/Create
        public IActionResult Create()
        {
            ViewData["Employee"] = _context.Employee.Where(e => e.DepId == 3).Include(de => de.Dep).Include(du => du.Duty).OrderByDescending(x => x.EmpName).ToList();
            return View();
        }

        // POST: ContactUs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ConId,ConTitle,ConName,ConEmail,ConPhone,ConContent")] ContactUs contactUs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contactUs);
                await _context.SaveChangesAsync();
                SendMail(contactUs);
                return RedirectToAction("Create");
            }
            return View(contactUs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Subscribe(string email)
        {
            ContactUs contactUs = new ContactUs();
            contactUs.ConTitle = "Subscribe";
            contactUs.ConName = "Anonymous";
            contactUs.ConEmail = email;
            if (ModelState.IsValid)
            {
                _context.Add(contactUs);
                await _context.SaveChangesAsync();
                SendMail(contactUs);
                return RedirectToAction("Index","Home");
            }
            return View(contactUs);
        }

        // GET: ContactUs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactUs = await _context.ContactUs.SingleOrDefaultAsync(m => m.ConId == id);
            if (contactUs == null)
            {
                return NotFound();
            }
            return View(contactUs);
        }

        // POST: ContactUs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ConId,ConTitle,ConName,ConEmail,ConPhone,ConContent")] ContactUs contactUs)
        {
            if (id != contactUs.ConId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactUs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactUsExists(contactUs.ConId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(contactUs);
        }

        // GET: ContactUs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactUs = await _context.ContactUs
                .SingleOrDefaultAsync(m => m.ConId == id);
            if (contactUs == null)
            {
                return NotFound();
            }

            return View(contactUs);
        }

        // POST: ContactUs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contactUs = await _context.ContactUs.SingleOrDefaultAsync(m => m.ConId == id);
            _context.ContactUs.Remove(contactUs);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ContactUsExists(int id)
        {
            return _context.ContactUs.Any(e => e.ConId == id);
        }

        private void SendMail(ContactUs contact)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("OMEGA", "contact@omega.com.vn"));
            message.To.Add(new MailboxAddress("info", "info@omega.com.vn"));
            message.Subject = "Liên hệ từ omegea.com.vn";

            message.Body = new TextPart("plain")
            {
                Text = @"Tiêu đề : " + contact.ConTitle + "\r\n" +
                "Tên người liên hệ : " + contact.ConName + "\r\n" +
                "Email : " + contact.ConEmail + "\r\n" +
                "SĐT : " + contact.ConPhone + "\r\n" + 
                "Nội dung : " + contact.ConContent + "\r\n\r\n" +
                "Mail được gửi tự động từ omega.com.vn"
            }; 

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect("mail.omega.com.vn", 587, false);

                client.AuthenticationMechanisms.Remove("XOAUTH2");

                client.Authenticate("contact@omega.com.vn", "chung@Omega123");

                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
