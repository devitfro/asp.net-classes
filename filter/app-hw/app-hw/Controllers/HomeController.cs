using app_hw.Data;
using app_hw.Filters;
using app_hw.Models;
using app_hw.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace app_hw.Controllers
{
    [ServiceFilter(typeof(LogActionFilter))]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly EmailService _emailService;

        public HomeController(ApplicationDbContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        [HttpGet]
        public IActionResult Index() => View();

        [HttpPost]
        public IActionResult Index(Letter letter)
        {
            if (ModelState.IsValid)
            {
                _context.Letters.Add(letter);
                _context.SaveChanges();

                _emailService.SendEmail(letter.Email,
                    "Ваше письмо FutureMe отправлено!",
                    $"Привет, {letter.Name}! Мы доставим твоё письмо {letter.DeliveryDate:dd.MM.yyyy}.\n\nТекст письма:\n{letter.Message}");

                return RedirectToAction("Success");
            }
            return View(letter);
        }

        public IActionResult Success() => View();
    }
}
