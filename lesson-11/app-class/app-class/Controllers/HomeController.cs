using System.Diagnostics;
using app_class.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace app_class.Controllers
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
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Success");
            }
            return View(product);
        }

        public IActionResult Success()
        {
            return Content("Продукт успешно добавлен!");
        }
    }
}
