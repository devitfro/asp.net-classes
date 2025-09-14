using app_class_2.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace app_class_2.Controllers
{
    public class UserController : Controller
    {
        public static List<User> _users = new();

        public IActionResult Index()
        {
            return View(_users);
        }

        public IActionResult Registration()
        {
            return View();
        }


        [HttpPost]
        public IActionResult RegisterPost(User user)
        {
            if (!ModelState.IsValid)
                return View("Registration", user);

            _users.Add(user);
            return RedirectToAction("Index");
        }
    }
}


