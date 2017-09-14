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

    public class ProductsController : Controller
    {
        private readonly OmegaProjectContext _context;

        public ProductsController(OmegaProjectContext context)
        {
            _context = context;    
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var omegaProjectContext = _context.Product.Include(p => p.Photo);
            return View(await omegaProjectContext.ToListAsync());
        }

        // GET: Products
        [Authorize]
        public async Task<IActionResult> List()
        {
            var products = _context.Product;
            return View(await products.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Photo)
                .SingleOrDefaultAsync(m => m.ProdId == id);
            if (product == null)
            {
                return NotFound();
            }
            Random r = new Random();
            var recentproduct = await _context.Product.OrderBy(c => r.Next()).Take(4).Include(p => p.Photo).ToListAsync();
            ViewBag.Product = product;
            ViewBag.RecentProduct = recentproduct;
            return View();
        }

        // GET: Products/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["PhotoId"] = new SelectList(_context.Photo, "PhotoId", "PhotoName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("ProdId,ProdNo,ProdName,ProdDescription,ProdDescription1,ProdDescription2,ProdDescription3,ProdDescription4,ProdNotes,ProdDisabled,PhotoId")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction("List");
            }
            ViewData["PhotoId"] = new SelectList(_context.Photo, "PhotoId", "PhotoName", product.PhotoId);
            return View(product);
        }

        // GET: Products/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.SingleOrDefaultAsync(m => m.ProdId == id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["PhotoId"] = new SelectList(_context.Photo, "PhotoId", "PhotoName", product.PhotoId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("ProdId,ProdNo,ProdName,ProdDescription,ProdDescription1,ProdDescription2,ProdDescription3,ProdDescription4,ProdNotes,ProdDisabled,PhotoId")] Product product)
        {
            if (id != product.ProdId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProdId))
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
            ViewData["PhotoId"] = new SelectList(_context.Photo, "PhotoId", "PhotoName", product.PhotoId);
            return View(product);
        }

        // GET: Products/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Photo)
                .SingleOrDefaultAsync(m => m.ProdId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.SingleOrDefaultAsync(m => m.ProdId == id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction("List");
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.ProdId == id);
        }
    }
}
