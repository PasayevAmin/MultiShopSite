using System.ComponentModel.DataAnnotations;

namespace Multishop.Areas.Manage.ViewModels.Slide
{
    public class CreateSlideVM
    {
        [Required]
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        [Required]
        public IFormFile Photo { get; set; }
    }
}
