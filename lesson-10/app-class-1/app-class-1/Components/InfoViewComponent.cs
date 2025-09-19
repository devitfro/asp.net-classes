using Microsoft.AspNetCore.Mvc;

namespace app_class_1.Components
{
    public class InfoViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string title = "О сайте", string description = "Короткая информация о сайте", string imagePath = "~/images/site-info.jpg")
        {
            var model = new SiteInfoModel
            {
                Title = title,
                Description = description,
                ImageUrl = imagePath
            };

            return View(model);
        }
    }

    public class SiteInfoModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; } // относительный путь типа "~/images/site-info.jpg"
    }
}
