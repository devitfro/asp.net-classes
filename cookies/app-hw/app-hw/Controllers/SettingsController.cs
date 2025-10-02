using app_hw.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace app_hw.Controllers
{
    public class SettingsController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var theme = Request.Cookies["Theme"] ?? "light";
            var model = new SettingsViewModel { Theme = theme };
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(SettingsViewModel model)
        {
            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(30)
            };

            Response.Cookies.Append("Theme", model.Theme, options);

            TempData["Message"] = "Настройки сохранены!";
            return RedirectToAction("Index", "Home");
        }
    }
}
