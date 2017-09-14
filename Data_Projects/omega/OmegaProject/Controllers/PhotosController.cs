using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OmegaProject.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;

namespace OmegaProject.Controllers
{
    [Authorize]
    public class PhotosController : Controller
    {
        private readonly OmegaProjectContext _context;
        private IHostingEnvironment _environment;

        public PhotosController(OmegaProjectContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Photos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Photo.OrderByDescending(p => p.PhotoId).ToListAsync());
        }

        // GET: Photos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photo = await _context.Photo
                .SingleOrDefaultAsync(m => m.PhotoId == id);
            if (photo == null)
            {
                return NotFound();
            }

            return View(photo);
        }

        // GET: Photos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Photos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PhotoId,PhotoName,PhotoNotes,PhotoDisabled")] Photo photo, IFormFile fileThumbNail, IFormFile fileLarge01, IFormFile fileLarge02, IFormFile fileLarge03, IFormFile fileLarge04, IFormFile fileLarge05)
        {
            if (ModelState.IsValid)
            {
                var uploads = Path.Combine(_environment.WebRootPath, "img\\upload");
                Random r = new Random();
                if (fileThumbNail != null && fileThumbNail.Length > 0)
                {
                    if (fileThumbNail.Length > 0)
                    {
                        var filename = r.Next(0,10000).ToString() + "_" + ContentDispositionHeaderValue.Parse(fileThumbNail.ContentDisposition).FileName.Trim('"');
                        using (var fileStream = new FileStream(Path.Combine(uploads, filename), FileMode.Create))
                        {
                            await fileThumbNail.CopyToAsync(fileStream);
                            photo.PhotoThumbNail = filename;
                        }
                    }
                }
                if (fileLarge01 != null && fileLarge01.Length > 0)
                {
                    if (fileLarge01.Length > 0)
                    {
                        var filename = r.Next(0, 10000).ToString() + "_" + ContentDispositionHeaderValue.Parse(fileLarge01.ContentDisposition).FileName.Trim('"');
                        using (var fileStream = new FileStream(Path.Combine(uploads, filename), FileMode.Create))
                        {
                            await fileLarge01.CopyToAsync(fileStream);
                            photo.PhotoLarge01 = filename;
                        }
                    }
                }
                if (fileLarge02 != null && fileLarge02.Length > 0)
                {
                    if (fileLarge02.Length > 0)
                    {
                        var filename = r.Next(0, 10000).ToString() + "_" + ContentDispositionHeaderValue.Parse(fileLarge02.ContentDisposition).FileName.Trim('"');
                        using (var fileStream = new FileStream(Path.Combine(uploads, filename), FileMode.Create))
                        {
                            await fileLarge02.CopyToAsync(fileStream);
                            photo.PhotoLarge02 = filename;
                        }
                    }
                }
                if (fileLarge03 != null && fileLarge03.Length > 0)
                {
                    if (fileLarge03.Length > 0)
                    {
                        var filename = r.Next(0, 10000).ToString() + "_" + ContentDispositionHeaderValue.Parse(fileLarge03.ContentDisposition).FileName.Trim('"');
                        using (var fileStream = new FileStream(Path.Combine(uploads, filename), FileMode.Create))
                        {
                            await fileLarge03.CopyToAsync(fileStream);
                            photo.PhotoLarge03 = filename;
                        }
                    }
                }
                if (fileLarge04 != null && fileLarge04.Length > 0)
                {
                    if (fileLarge04.Length > 0)
                    {
                        var filename = r.Next(0, 10000).ToString() + "_" + ContentDispositionHeaderValue.Parse(fileLarge04.ContentDisposition).FileName.Trim('"');
                        using (var fileStream = new FileStream(Path.Combine(uploads, filename), FileMode.Create))
                        {
                            await fileLarge04.CopyToAsync(fileStream);
                            photo.PhotoLarge04 = filename;
                        }
                    }
                }
                if (fileLarge05 != null && fileLarge05.Length > 0)
                {
                    if (fileLarge05.Length > 0)
                    {
                        var filename = r.Next(0, 10000).ToString() + "_" + ContentDispositionHeaderValue.Parse(fileLarge05.ContentDisposition).FileName.Trim('"');
                        using (var fileStream = new FileStream(Path.Combine(uploads, filename), FileMode.Create))
                        {
                            await fileLarge05.CopyToAsync(fileStream);
                            photo.PhotoLarge05 = filename;
                        }
                    }
                }
                _context.Add(photo);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(photo);
        }

        // GET: Photos/CreateDirect
        public IActionResult CreateDirect()
        {
            return View();
        }

        // POST: Photos/CreateDirect
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDirect([Bind("PhotoId,PhotoName,PhotoNotes,PhotoDisabled")] Photo photo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(photo);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(photo);
        }

        // GET: Photos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photo = await _context.Photo.SingleOrDefaultAsync(m => m.PhotoId == id);
            if (photo == null)
            {
                return NotFound();
            }
            return View(photo);
        }

        // POST: Photos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PhotoId,PhotoName,PhotoThumbNail,PhotoLarge01,PhotoLarge02,PhotoLarge03,PhotoLarge04,PhotoLarge05,PhotoNotes,PhotoDisabled")] Photo photo, IFormFile fileThumbNail, IFormFile fileLarge01, IFormFile fileLarge02, IFormFile fileLarge03, IFormFile fileLarge04, IFormFile fileLarge05)
        {
            if (id != photo.PhotoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var uploads = Path.Combine(_environment.WebRootPath, "img\\upload");
                    Random r = new Random();
                    if (fileThumbNail != null && fileThumbNail.Length > 0)
                    {
                        if (fileThumbNail.Length > 0)
                        {
                            var filename = r.Next(0, 10000).ToString() + "_" + ContentDispositionHeaderValue.Parse(fileThumbNail.ContentDisposition).FileName.Trim('"');
                            using (var fileStream = new FileStream(Path.Combine(uploads, filename), FileMode.Create))
                            {
                                await fileThumbNail.CopyToAsync(fileStream);
                                photo.PhotoThumbNail = filename;
                            }
                        }
                    }
                    if (fileLarge01 != null && fileLarge01.Length > 0)
                    {
                        if (fileLarge01.Length > 0)
                        {
                            var filename = r.Next(0, 10000).ToString() + "_" + ContentDispositionHeaderValue.Parse(fileLarge01.ContentDisposition).FileName.Trim('"');
                            using (var fileStream = new FileStream(Path.Combine(uploads, filename), FileMode.Create))
                            {
                                await fileLarge01.CopyToAsync(fileStream);
                                photo.PhotoLarge01 = filename;
                            }
                        }
                    }
                    if (fileLarge02 != null && fileLarge02.Length > 0)
                    {
                        if (fileLarge02.Length > 0)
                        {
                            var filename = r.Next(0, 10000).ToString() + "_" + ContentDispositionHeaderValue.Parse(fileLarge02.ContentDisposition).FileName.Trim('"');
                            using (var fileStream = new FileStream(Path.Combine(uploads, filename), FileMode.Create))
                            {
                                await fileLarge02.CopyToAsync(fileStream);
                                photo.PhotoLarge02 = filename;
                            }
                        }
                    }
                    if (fileLarge03 != null && fileLarge03.Length > 0)
                    {
                        if (fileLarge03.Length > 0)
                        {
                            var filename = r.Next(0, 10000).ToString() + "_" + ContentDispositionHeaderValue.Parse(fileLarge03.ContentDisposition).FileName.Trim('"');
                            using (var fileStream = new FileStream(Path.Combine(uploads, filename), FileMode.Create))
                            {
                                await fileLarge03.CopyToAsync(fileStream);
                                photo.PhotoLarge03 = filename;
                            }
                        }
                    }
                    if (fileLarge04 != null && fileLarge04.Length > 0)
                    {
                        if (fileLarge04.Length > 0)
                        {
                            var filename = r.Next(0, 10000).ToString() + "_" + ContentDispositionHeaderValue.Parse(fileLarge04.ContentDisposition).FileName.Trim('"');
                            using (var fileStream = new FileStream(Path.Combine(uploads, filename), FileMode.Create))
                            {
                                await fileLarge04.CopyToAsync(fileStream);
                                photo.PhotoLarge04 = filename;
                            }
                        }
                    }
                    if (fileLarge05 != null && fileLarge05.Length > 0)
                    {
                        if (fileLarge05.Length > 0)
                        {
                            var filename = r.Next(0, 10000).ToString() + "_" + ContentDispositionHeaderValue.Parse(fileLarge05.ContentDisposition).FileName.Trim('"');
                            using (var fileStream = new FileStream(Path.Combine(uploads, filename), FileMode.Create))
                            {
                                await fileLarge05.CopyToAsync(fileStream);
                                photo.PhotoLarge05 = filename;
                            }
                        }
                    }
                    _context.Update(photo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhotoExists(photo.PhotoId))
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
            return View(photo);
        }
        // GET: Photos/EditDirect/5
        public async Task<IActionResult> EditDirect(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photo = await _context.Photo.SingleOrDefaultAsync(m => m.PhotoId == id);
            if (photo == null)
            {
                return NotFound();
            }
            return View(photo);
        }

        // POST: Photos/EditDirect/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDirect(int id, [Bind("PhotoId,PhotoName,PhotoThumbNail,PhotoLarge01,PhotoLarge02,PhotoLarge03,PhotoLarge04,PhotoLarge05,PhotoNotes,PhotoDisabled")] Photo photo)
        {
            if (id != photo.PhotoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(photo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhotoExists(photo.PhotoId))
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
            return View(photo);
        }
        // GET: Photos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photo = await _context.Photo
                .SingleOrDefaultAsync(m => m.PhotoId == id);
            if (photo == null)
            {
                return NotFound();
            }

            return View(photo);
        }

        // POST: Photos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var photo = await _context.Photo.SingleOrDefaultAsync(m => m.PhotoId == id);
            _context.Photo.Remove(photo);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PhotoExists(int id)
        {
            return _context.Photo.Any(e => e.PhotoId == id);
        }
    }
}
