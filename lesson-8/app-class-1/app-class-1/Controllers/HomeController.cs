using app_class_1.Models;
using app_class_1.Services;
using app_class_1.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace app_class_1.Controllers
{
    public class HomeController : Controller
    {
        private readonly PhoneService _phoneService;

        public HomeController(PhoneService phoneService)
        {
            _phoneService = phoneService;
        }

        private List<CompanyViewModel> GetCompanies()
        {
            List<CompanyViewModel> compModels = _phoneService.GetCompanies()
                .Select(c => new CompanyViewModel { Id = c.Id, Name = c.Name })
                .ToList();

            compModels.Insert(0, new CompanyViewModel { Id = 0, Name = "Все" });
            return compModels;
        }

        public IActionResult Index(int? companyId)
        {
            IndexViewModel ivm = new IndexViewModel { Companies = GetCompanies(), Phones = _phoneService.GetPhones() };

            if (companyId != null && companyId > 0)
            {
                ivm.SelectedCompanyId = companyId;
                ivm.Phones = _phoneService.GetPhones().Where(p => p.Manufacturer.Id == companyId);
            }
            return View(ivm);
        }

        public IActionResult CreatePhone()
        {
            ViewBag.Companies = GetCompanies();
            return View();
        }

        [HttpPost]
        public IActionResult CreatePhone(PhoneViewModel phone)
        {
            if (ModelState.IsValid)
            {
                _phoneService.AddPhone(new Phone
                {
                    Name = phone.Name,
                    Price = phone.Price,
                    Manufacturer = _phoneService.GetCompany(phone.CompanyId)
                });
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Companies = GetCompanies();
            return View(phone);
        }
    }
}
