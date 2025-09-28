using app_hw.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace app_hw.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : Controller
    {
        private readonly WeatherService _weatherService;

        public WeatherController(WeatherService weatherService)
        {
            _weatherService = weatherService;
        }


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                ViewBag.Error = "Введите название города";
                return View();
            }

            var weather = await _weatherService.GetWeatherAsync(city);

            if (weather == null)
            {
                ViewBag.Error = "Не удалось получить данные о погоде";
                return View();
            }

            return View(weather);
        }

    }
}
