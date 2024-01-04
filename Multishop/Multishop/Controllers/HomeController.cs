using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Multishop.DAL;
using Multishop.Entities;
using Multishop.ViewModels;

namespace Multishop.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;

        }
        public async Task<IActionResult> Index()
        {
            List<Slide> slides = await _context.Slides.ToListAsync();
            List<Product> products = await _context.Products.Include(x => x.ProductImages).Include(x=>x.Category).ToListAsync();
            List<Product> lastproducts = await _context.Products.Include(x => x.ProductImages).OrderByDescending(x => x.Id).Take(8).ToListAsync();
            List<Category> categories=await _context.Categories.ToListAsync();
           
            HomeVM vm = new HomeVM
            {
                Slides = slides,
                Products = products,
                LastestProducts = lastproducts,
                Categories = categories
            };
            return View(vm);
        }
        public async Task<IActionResult> ErrorPage(string error = "it stopped")
        {
            return View(model: error);
        }
    }
}
