using Microsoft.AspNetCore.Mvc;

namespace app_class_1.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
