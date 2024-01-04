using Multishop.Entities;
using System.Drawing;
using Color = Multishop.Entities.Color;

namespace Multishop.ViewModels
{
    public class ProductVM
    {
        public Product Products { get; set; }
        public List<Product> RelatedProducts { get; set; }
        public List<Product> LastestProducts { get; set; }
        public List<Product> CategoryProduct { get; set; }



        public List<ProductImage> ProductImages { get; set; }
        public List<Color> Colors { get; set; }
        public List<ProductColor> ProductColors { get; set; }
        public List<Category> Categories { get; set; }
    }
}
