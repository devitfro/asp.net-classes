using Microsoft.AspNetCore.Mvc;
using app_hw_exchange.Services;

namespace app_hw_exchange.Controllers
{
    public class HomeController : Controller
    {
        private readonly ExchangeRateService _exchangeService;

        public HomeController(ExchangeRateService exchangeService)
        {
            _exchangeService = exchangeService;
        }

        public IActionResult Index()
        {
            ViewBag.Currencies = _exchangeService.GetAvailableCurrencies();
            return View();
        }

        [HttpPost]
        public IActionResult Convert(string FromCurrency, string ToCurrency, float Amount)
        {
            ViewBag.Currencies = _exchangeService.GetAvailableCurrencies();

            if (Enum.TryParse<Currency>(FromCurrency, out var from) &&
                Enum.TryParse<Currency>(ToCurrency, out var to))
            {
                float result = _exchangeService.GetRate(from, to) * Amount;
                ViewBag.Result = $"{Amount} {from} = {result:F2} {to}";
            }
            else
            {
                ViewBag.Result = "Invalid currency selected!";
            }

            return View("Index");
        }
    }
}
