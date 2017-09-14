using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OmegaProject.Models;
using Microsoft.AspNetCore.Authorization;

namespace OmegaProject.Controllers
{
    public class CustomersController : Controller
    {
        private readonly OmegaProjectContext _context;

        public CustomersController(OmegaProjectContext context)
        {
            _context = context;    
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            List<Branch> branchs = await _context.Branch.Where(b => b.BraDisabled == false).OrderBy(b => b.BraName).ToListAsync();
            List<Customer> customers = await _context.Customer.OrderBy(c => c.CusId).Include(c => c.Bra).Include(c => c.Photo).Include(c => c.Status).ToListAsync();
            ViewBag.Branchs = branchs;
            ViewBag.Customers = customers;
            return View();
        }

        // GET: Customers by Branch
        public async Task<IActionResult> Branches(int? id)
        {
            List<Branch> branchs = await _context.Branch.Where(b => b.BraDisabled == false).OrderBy(b => b.BraName).ToListAsync();
            List<Customer> customers = new List<Customer>();
            if (id != null && id != 0)
            {
                customers = await _context.Customer.Where(c => c.BraId == id).OrderBy(c => c.CusName).Include(c => c.Bra).Include(c => c.Photo).Include(c => c.Status).ToListAsync();
            }
            else
            {
                return RedirectToAction("Index");
            }
            ViewBag.Branchs = branchs;
            ViewBag.Customers = customers;
            return View();
        }

        // GET: Customers
        [Authorize]
        public async Task<IActionResult> List()
        {
            var customers = _context.Customer.OrderByDescending(c => c.CusId).Include(c => c.Bra).Include(c => c.Status);
            return View(await customers.ToListAsync());
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .Include(c => c.Bra)
                .Include(c => c.Photo)
                .Include(c => c.Status)
                .SingleOrDefaultAsync(m => m.CusId == id);
            if (customer == null)
            {
                return NotFound();
            }
            Random r = new Random();
            List<Customer> customers = await _context.Customer.Where(c => c.BraId == customer.BraId).OrderBy(c => r.Next()).Take(4).Include(p => p.Photo).ToListAsync();
            ViewBag.Customer = customer;
            ViewBag.Customers = customers;
            return View();
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            ViewData["BraId"] = new SelectList(_context.Branch, "BraId", "BraName");
            ViewData["PhotoId"] = new SelectList(_context.Photo, "PhotoId", "PhotoName");
            ViewData["StatusId"] = new SelectList(_context.Status, "StatusId", "StatusName");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CusId,CusNo,CusName,CusAddress,CusWeb,CusNotes,BraId,StatusId,PhotoId")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.CusDateCreate = DateTime.Now;
                customer.CusLastUpdate = DateTime.Now;
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction("List");
            }
            ViewData["BraId"] = new SelectList(_context.Branch, "BraId", "BraName", customer.BraId);
            ViewData["PhotoId"] = new SelectList(_context.Photo, "PhotoId", "PhotoName", customer.PhotoId);
            ViewData["StatusId"] = new SelectList(_context.Status, "StatusId", "StatusName", customer.StatusId);
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer.SingleOrDefaultAsync(m => m.CusId == id);
            if (customer == null)
            {
                return NotFound();
            }
            ViewData["BraId"] = new SelectList(_context.Branch, "BraId", "BraName", customer.BraId);
            ViewData["PhotoId"] = new SelectList(_context.Photo, "PhotoId", "PhotoName", customer.PhotoId);
            ViewData["StatusId"] = new SelectList(_context.Status, "StatusId", "StatusName", customer.StatusId);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CusId,CusNo,CusName,CusAddress,CusWeb,CusNotes,CusDateCreate,BraId,StatusId,PhotoId")] Customer customer)
        {
            if (id != customer.CusId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    customer.CusLastUpdate = DateTime.Now;
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CusId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("List");
            }
            ViewData["BraId"] = new SelectList(_context.Branch, "BraId", "BraName", customer.BraId);
            ViewData["PhotoId"] = new SelectList(_context.Photo, "PhotoId", "PhotoName", customer.PhotoId);
            ViewData["StatusId"] = new SelectList(_context.Status, "StatusId", "StatusName", customer.StatusId);
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .Include(c => c.Bra)
                .Include(c => c.Photo)
                .Include(c => c.Status)
                .SingleOrDefaultAsync(m => m.CusId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customer.SingleOrDefaultAsync(m => m.CusId == id);
            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction("List");
        }

        private bool CustomerExists(int id)
        {
            return _context.Customer.Any(e => e.CusId == id);
        }
    }
}
