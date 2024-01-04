using System.ComponentModel.DataAnnotations;

namespace Multishop.Areas.Manage.ViewModels
{
    public class CreateColorVM
    {
        [Required(ErrorMessage = "Name must be entered")]
        [MaxLength(25, ErrorMessage = "Name cannot be longer than 25")]
        public string Name { get; set; }
    }
}
