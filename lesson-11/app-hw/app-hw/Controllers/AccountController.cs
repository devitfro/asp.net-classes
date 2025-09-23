using app_hw.Data;
using app_hw.Models;
using app_hw.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;

namespace app_hw.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationContext _dbContext;

        public AccountController(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Username = model.Username,
                Age = model.Age,
                PasswordHash = HashPassword(model.Password),
                CreditCardNumber = model.CreditCardNumber,
                Website = model.Website
            };

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            ViewBag.Message = "Регистрация прошла успешно!";
            return Content("Success");
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult IsUsernameAvailable(string username)
        {
            bool isAvailable = username != "admin";

            if (isAvailable)
                return Json(true);
            else
                return Json($"Имя пользователя '{username}' уже занято.");
        }
        public string HashPassword(string password)
        {
            var hasher = new PasswordHasher<User>();
            return hasher.HashPassword(null, password);
        }

    }
}
