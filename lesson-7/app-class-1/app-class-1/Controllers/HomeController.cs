using System.Diagnostics;
using app_class_1.Models;
using app_class_1.Services;
using Microsoft.AspNetCore.Mvc;

namespace app_class_1.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                TempData["UserChanged"] = true;
                return RedirectToAction(nameof(Index));

            }
            return View(userViewModel);
        }
    }
}
