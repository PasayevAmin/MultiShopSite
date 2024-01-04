using Multishop.Entities;
using System.ComponentModel.DataAnnotations;
using Color = Multishop.Entities.Color;

namespace Multishop.Areas.Manage.ViewModels
{
    public class UpdateProductVM
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public List<ProductImage>? ProductImages { get; set; }
        public IFormFile? MainPhoto { get; set; }
        public IFormFile? HoverPhoto { get; set; }
        public List<IFormFile>? Photos { get; set; }
        public List<Category>? Categories { get; set; }
        public List<int>? ImageIds { get; set; }
        public List<int> ColorIds { get; set; }
        public List<Color>? Colors { get; set; }
    }
}
