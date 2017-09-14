using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OmegaProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace OmegaProject.Controllers
{
    public class PostsController : Controller
    {
        private readonly OmegaProjectContext _context;
        private readonly SignInManager<ApplicationUser> _SignInManager;
        private readonly UserManager<ApplicationUser> _UserManager;

        public PostsController(OmegaProjectContext context, SignInManager<ApplicationUser> SignInManager, UserManager<ApplicationUser> UserManager)
        {
            _context = context;
            _SignInManager = SignInManager;
            _UserManager = UserManager;
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            Random r = new Random();
            List<Post> posts = await _context.Post.OrderByDescending(p => p.PostId).Take(20).Include(p => p.Photo).Include(p => p.Category).Include(p => p.PostUserCreateNavigation).Include(p => p.PostUserUpdateNavigation).Include(p => p.Status).ToListAsync();
            List<Post> rposts = await _context.Post.OrderBy(p => r.Next()).Take(5).Include(p => p.Photo).ToListAsync();
            List<Category> categories = await _context.Category.Where(c => c.CateDisabled == false).ToListAsync();
            List<string> tags = await _context.Post.Select(t => t.PostLabel).Distinct().ToListAsync();
            ViewBag.Categories = categories;
            ViewBag.Posts = posts;
            ViewBag.RandomPosts = rposts;
            ViewBag.Tags = tags;
            return View();
        }

        // GET: Posts
        [Authorize]
        public async Task<IActionResult> List()
        {
            var posts = _context.Post.OrderByDescending(p => p.PostId).Include(p => p.Category).Include(p => p.PostUserCreateNavigation).Include(p => p.Status);
            return View(await posts.ToListAsync());
        }

        // GET: Posts/Category/1
        public async Task<IActionResult> Categories(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Random r = new Random();
            List<Post> posts = await _context.Post.Where(p => p.CategoryId == id).OrderByDescending(p => p.PostId).Take(20).Include(p => p.Photo).Include(p => p.Category).Include(p => p.PostUserCreateNavigation).Include(p => p.PostUserUpdateNavigation).Include(p => p.Status).ToListAsync();
            List<Post> rposts = await _context.Post.OrderBy(p => r.Next()).Take(5).Include(p => p.Photo).ToListAsync();
            List<Category> categories = await _context.Category.Where(c => c.CateDisabled == false).ToListAsync();
            List<string> tags = await _context.Post.Select(t => t.PostLabel).Distinct().ToListAsync();
            ViewBag.Categories = categories;
            ViewBag.Posts = posts;
            ViewBag.RandomPosts = rposts;
            ViewBag.Tags = tags;
            return View();
        }

        // GET: Posts/Category/1
        public async Task<IActionResult> Tags(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }
            Random r = new Random();
            List<Post> posts = await _context.Post.Where(p => p.PostLabel.Contains(id)).OrderByDescending(p => p.PostId).Take(20).Include(p => p.Photo).Include(p => p.Category).Include(p => p.PostUserCreateNavigation).Include(p => p.PostUserUpdateNavigation).Include(p => p.Status).ToListAsync();
            List<Post> rposts = await _context.Post.OrderBy(p => r.Next()).Take(5).Include(p => p.Photo).ToListAsync();
            List<Category> categories = await _context.Category.Where(c => c.CateDisabled == false).ToListAsync();
            List<string> tags = await _context.Post.Select(t => t.PostLabel).Distinct().ToListAsync();
            ViewBag.Categories = categories;
            ViewBag.Posts = posts;
            ViewBag.RandomPosts = rposts;
            ViewBag.Tags = tags;
            return View();
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post
                .Include(p => p.Category)
                .Include(p => p.Photo)
                .Include(p => p.PostUserCreateNavigation)
                .Include(p => p.PostUserUpdateNavigation)
                .Include(p => p.Status)
                .SingleOrDefaultAsync(m => m.PostId == id);
            if (post == null)
            {
                return NotFound();
            }
            Random r = new Random();
            List<Post> rposts = await _context.Post.OrderBy(p => r.Next()).Take(5).Include(p => p.Photo).ToListAsync();
            List<Category> categories = await _context.Category.Where(c => c.CateDisabled == false).ToListAsync();
            List<string> tags = await _context.Post.Select(t => t.PostLabel).Distinct().ToListAsync();
            int next = await _context.Post.OrderBy(p => p.PostId).Where(p => p.PostId > id).Select(p => p.PostId).FirstOrDefaultAsync();
            int previous = await _context.Post.OrderByDescending(p => p.PostId).Where(p => p.PostId < id).Select(p => p.PostId).FirstOrDefaultAsync();
            ViewBag.Categories = categories;
            ViewBag.RandomPosts = rposts;
            ViewBag.Tags = tags;
            ViewBag.Post = post;
            ViewBag.Previous = previous;
            ViewBag.Next = next;
            return View();
        }

        // GET: Posts/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "CateId", "CateName");
            ViewData["PhotoId"] = new SelectList(_context.Photo, "PhotoId", "PhotoName");
            ViewData["StatusId"] = new SelectList(_context.Status, "StatusId", "StatusName");
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("PostId,PostTitle,PostContent,PostDescription,PostLabel,PostDisabled,StatusId,CategoryId,PhotoId")] Post post)
        {
            if (ModelState.IsValid)
            {
                post.PostDateCreate = DateTime.Now;
                post.PostLastUpdate = DateTime.Now;
                if (_SignInManager.IsSignedIn(User))
                {
                    post.PostUserCreate = _UserManager.GetUserId(User);
                    post.PostUserUpdate = _UserManager.GetUserId(User);
                }
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction("List");
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "CateId", "CateName", post.CategoryId);
            ViewData["PhotoId"] = new SelectList(_context.Photo, "PhotoId", "PhotoName", post.PhotoId);
            ViewData["StatusId"] = new SelectList(_context.Status, "StatusId", "StatusName", post.StatusId);
            return View(post);
        }

        // GET: Posts/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post.SingleOrDefaultAsync(m => m.PostId == id);
            if (post == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "CateId", "CateName", post.CategoryId);
            ViewData["PhotoId"] = new SelectList(_context.Photo, "PhotoId", "PhotoName", post.PhotoId);
            ViewData["StatusId"] = new SelectList(_context.Status, "StatusId", "StatusName", post.StatusId);
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("PostId,PostTitle,PostContent,PostDescription,PostLabel,PostDisabled,StatusId,CategoryId,PhotoId,PostDateCreate,PostUserCreate")] Post post)
        {
            if (id != post.PostId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    post.PostLastUpdate = DateTime.Now;
                    if (_SignInManager.IsSignedIn(User))
                    {
                        post.PostUserUpdate = _UserManager.GetUserId(User);
                    }
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.PostId))
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
            ViewData["CategoryId"] = new SelectList(_context.Category, "CateId", "CateName", post.CategoryId);
            ViewData["PhotoId"] = new SelectList(_context.Photo, "PhotoId", "PhotoName", post.PhotoId);
            ViewData["StatusId"] = new SelectList(_context.Status, "StatusId", "StatusName", post.StatusId);
            return View(post);
        }

        // GET: Posts/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post
                .Include(p => p.Category)
                .Include(p => p.Photo)
                .Include(p => p.PostUserCreateNavigation)
                .Include(p => p.PostUserUpdateNavigation)
                .Include(p => p.Status)
                .SingleOrDefaultAsync(m => m.PostId == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Post.SingleOrDefaultAsync(m => m.PostId == id);
            _context.Post.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction("List");
        }

        private bool PostExists(int id)
        {
            return _context.Post.Any(e => e.PostId == id);
        }
    }
}
