using app_hw.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index() => View();

        [Authorize]
        public IActionResult Profile()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            if (userIdClaim == null) return RedirectToAction("Login", "Account");

            var user = _context.Users.FirstOrDefault(u => u.Id == int.Parse(userIdClaim));
            return View(user);
        }
    }
}
