using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OmegaProject.Models;
using System.Collections.Generic;
using System;

namespace OmegaProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly OmegaProjectContext _Context;

        public HomeController(OmegaProjectContext Context)
        {
            _Context = Context;
        }

        public async Task<IActionResult> Index()
        {
            Random r = new Random();
            List<Event> events = await _Context.Event.OrderBy(e => e.EventId).Include(p => p.Photo).ToListAsync();
            List<Product> products = await _Context.Product.Take(6).Include(p => p.Photo).ToListAsync();
            List<Customer> customers = await _Context.Customer.OrderBy(c => r.Next()).Take(18).Include(p => p.Photo).ToListAsync();
            ViewBag.Events = events;
            ViewBag.Products = products;
            ViewBag.Customers = customers;
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public async Task<IActionResult> Deployment()
        {
            var employees = _Context.Employee.Include(de => de.Dep).Include(du => du.Duty).Include(p => p.Photo);
            return View(await employees.Where(e => e.DepId == 5).Take(6).ToListAsync());
        }

        public async Task<IActionResult> CustomerService()
        {
            var employees = _Context.Employee.Include(de => de.Dep).Include(du => du.Duty).Include(p => p.Photo);
            return View(await employees.Where(e => e.DepId == 4 || e.DepId == 1).Take(6).ToListAsync());
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
