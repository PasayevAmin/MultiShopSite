using Multishop.Entities;
using System.Net.NetworkInformation;

namespace Multishop.ViewModels
{
    public class HomeVM
    {
        public List<Slide> Slides { get; set; }
        public List<Product> Products { get; set; }
        public List<Product> LastestProducts { get; set; }
        public List<Category> Categories { get; set; } 
    }
}
