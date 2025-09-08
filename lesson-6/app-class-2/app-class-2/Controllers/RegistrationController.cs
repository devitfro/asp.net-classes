using Microsoft.AspNetCore.Mvc;

namespace app_class_2.Controllers
{
    public class RegistrationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string GetUser(string name, string password)
        {
            return $"Name: {name}, Password: {password}";
        }
    }
}
