using System.Diagnostics;
using app_class_2.Models;
using Microsoft.AspNetCore.Mvc;

namespace app_class_2.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
