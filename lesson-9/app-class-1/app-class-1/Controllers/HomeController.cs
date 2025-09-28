using System.Diagnostics;
using app_class_1.Models;
using Microsoft.AspNetCore.Mvc;

namespace app_class_1.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            return Content(user.ToString());
        }

    }
}
