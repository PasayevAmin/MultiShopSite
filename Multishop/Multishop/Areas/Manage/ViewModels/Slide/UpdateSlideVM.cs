namespace Multishop.Areas.Manage.ViewModels.Slide
{
    public class UpdateSlideVM
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public string? Image { get; set; }

        public IFormFile? Photo { get; set; }
    }
}
