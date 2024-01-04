using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Multishop.Areas.Manage.Controllers;
using Multishop.DAL;
using Multishop.Entities;
using Multishop.Utilities.Exseptions;
using Multishop.ViewModels;

namespace Multishop.Controllers
{
    public class ProductController : Controller
    {

        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;

        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
        public async  Task<IActionResult> Details(int id)
        {
            if (id <= 0) throw new WrongRequestExceptions("Wrong Search");


            Product product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductImages)
                .Include(x => x.ProductColors).ThenInclude(x => x.Color)
                .FirstOrDefaultAsync(x => x.Id == id);


            if (product == null) throw new NotFoundExceptions("No product found");

            List<Product> relatedproducts = await _context.Products
                .Include(x => x.Category)
                .Include(x => x.ProductImages)
                .Where(p => p.Id != id)
                .Where(p => p.CategoryId == product.CategoryId)
                .Take(12)
                .ToListAsync();

            ProductVM productVM = new ProductVM
            {

                Products = product,
                RelatedProducts = relatedproducts,

            };

            return View(productVM);
        }
        public async Task<IActionResult> Shop(int? order)
        {

            IQueryable<Product> query= _context.Products.Include(x=>x.ProductImages).AsQueryable();
            switch (order)
            {
                case 1:
                    query = query.OrderBy(x => x.Name);
                    break;
                case 2:
                    query=  query.OrderBy(x => x.Price); 
                    break;
                case 3:
                    query = query.OrderByDescending(x => x.Id);
                    break;


            }

            ShopVM productVM = new ShopVM
            {

                Products = await query.ToListAsync(),

            };

            return View(productVM);
        }
    }
}
