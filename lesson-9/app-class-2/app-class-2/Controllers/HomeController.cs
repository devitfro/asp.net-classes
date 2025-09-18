using Microsoft.AspNetCore.Mvc;
using app_class_2.Models;

namespace app_class_2.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Form()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Form(Payer payer)
        {
            return View("Result", payer);
        }
    }
}
