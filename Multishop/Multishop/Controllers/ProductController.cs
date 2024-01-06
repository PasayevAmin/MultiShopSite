using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;    
using Multishop.Areas.Manage.Controllers;
using Multishop.Areas.Manage.ViewModels;
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
        public async Task<IActionResult> Shop(int page=1,int? id=null,int order=1)
        {

            IQueryable<Product> query= _context.Products.Include(x=>x.ProductImages).Include(x=>x.Category).AsQueryable();
            if (id is not null)
            {
                query = query.Skip((page - 1) * 5).Take(5).Include(x => x.ProductImages.Where(p => p.IsPrimary == true));
            }
            else
            {
                query = query.Where(p => p.CategoryId == id).Skip((page - 1) * 5).Take(5).Include(x => x.ProductImages.Where(p => p.IsPrimary == true));
            }
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

            PaginationVM<Product> vm = new PaginationVM<Product>
            {
                CurrentPage = page,
                Items = query.ToList(),
                TotalPage = page,
                Order = order,

            };
            
            

            return View(vm);
        }
    }
}
