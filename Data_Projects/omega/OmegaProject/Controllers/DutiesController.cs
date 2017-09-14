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
    [Authorize]
    public class DutiesController : Controller
    {
        private readonly OmegaProjectContext _context;

        public DutiesController(OmegaProjectContext context)
        {
            _context = context;    
        }

        // GET: Duties
        public async Task<IActionResult> Index()
        {
            return View(await _context.Duty.OrderByDescending(d => d.DutyId).ToListAsync());
        }

        // GET: Duties/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var duty = await _context.Duty
                .SingleOrDefaultAsync(m => m.DutyId == id);
            if (duty == null)
            {
                return NotFound();
            }

            return View(duty);
        }

        // GET: Duties/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Duties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DutyId,DutyNo,DutyName,DutyNotes,DutyDisabled")] Duty duty)
        {
            if (ModelState.IsValid)
            {
                _context.Add(duty);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(duty);
        }

        // GET: Duties/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var duty = await _context.Duty.SingleOrDefaultAsync(m => m.DutyId == id);
            if (duty == null)
            {
                return NotFound();
            }
            return View(duty);
        }

        // POST: Duties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DutyId,DutyNo,DutyName,DutyNotes,DutyDisabled")] Duty duty)
        {
            if (id != duty.DutyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(duty);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DutyExists(duty.DutyId))
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
            return View(duty);
        }

        // GET: Duties/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var duty = await _context.Duty
                .SingleOrDefaultAsync(m => m.DutyId == id);
            if (duty == null)
            {
                return NotFound();
            }

            return View(duty);
        }

        // POST: Duties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var duty = await _context.Duty.SingleOrDefaultAsync(m => m.DutyId == id);
            _context.Duty.Remove(duty);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool DutyExists(int id)
        {
            return _context.Duty.Any(e => e.DutyId == id);
        }
    }
}
