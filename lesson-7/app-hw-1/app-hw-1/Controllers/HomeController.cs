using System.Diagnostics;
using app_hw_1.Models;
using app_hw_1.Services;
using Microsoft.AspNetCore.Mvc;

namespace app_hw_1.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserService _userService;

        public HomeController(UserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            // 1 способ - ViewData
            //List<User> users = _userService.GetUsers();
            //ViewData["Users"] = users;

            // 2 способ - ViewBag
            //ViewBag.Users = _userService.GetUsers();

            // 3 способ - Model
            //var usersModel = _userService.GetUsers();
            //return View(usersModel);

            return View();
        }

        [HttpPost]
        public IActionResult Index(User user)
        {
            if (ModelState.IsValid)
            {
                _userService.AddUser(user);

                return RedirectToAction(nameof(ShowUsers));
            }
            return View(user);
        }

        public IActionResult ShowUsers()
        {
            var users = _userService.GetUsers();
            ViewData["Users"] = users;
            ViewBag.Users = users;
            return View(users);
        }
    }
}
