using System.Diagnostics;
using app_class_1.Models;
using Microsoft.AspNetCore.Mvc;

namespace app_class_1.Controllers
{
    public interface ITimeService
    {
        string Time { get; }
    }

    public class SimpleTimeService : ITimeService
    {
        public SimpleTimeService()
        {
            Time = DateTime.Now.ToString("hh:mm:ss");
        }
        public string Time { get; }
    }

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ITimeService ts = HttpContext.RequestServices.GetService<ITimeService>()!;
            return Content(ts.Time);
        }
    }
}
