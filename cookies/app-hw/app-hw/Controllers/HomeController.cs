using app_hw.Models;
using app_hw.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace app_hw.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;

        public HomeController(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var newsList = new List<News>
            {
                new News { Id = 1, Title = "Первая новость", Content = "Новость 1...", Date = DateTime.Now },
                new News { Id = 2, Title = "Вторая новость", Content = "Новость 2...", Date = DateTime.Now.AddDays(-1) },
            };

            var viewModel = _mapper.Map<List<NewsViewModel>>(newsList);

            var theme = Request.Cookies["Theme"] ?? "light";
            ViewBag.Theme = theme;

            return View(viewModel);
        }
    }
}
