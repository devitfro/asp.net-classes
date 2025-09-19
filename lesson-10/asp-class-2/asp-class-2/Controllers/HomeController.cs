using asp_class_2.Data;
using asp_class_2.Models;
using asp_class_2.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace asp_class_2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationContext _context;

        public HomeController(ApplicationContext context)
        {
            _context = context;
        }

        //public async Task<IActionResult> Index()
        //{
        //    var users = await _context.Users.ToListAsync();
        //    return View(users);
        //}

        //public async Task<IActionResult> Index(SortState sortOrder = SortState.NameAsc)
        //{
        //    IQueryable<User> users = _context.Users.Include(e => e.Company);
        //    ViewData["NameSort"] = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
        //    ViewData["AgeSort"] = sortOrder == SortState.AgeAsc ? SortState.AgeDesc : SortState.AgeAsc;

        //    users = sortOrder switch
        //    {
        //        SortState.NameDesc => users.OrderByDescending(e => e.Name),
        //        SortState.AgeAsc => users.OrderBy(e => e.Age),
        //        SortState.AgeDesc => users.OrderByDescending(e => e.Age),
        //        _ => users.OrderBy(s => s.Name),
        //    };

        //    return View(await users.AsNoTracking().ToListAsync());
        //}

        public async Task<IActionResult> Index(int? company, string name, SortState sortOrder = SortState.NameAsc)
        {
            IQueryable<User> users = _context.Users.Include(e => e.Company);
            ViewData["NameSort"] = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            ViewData["AgeSort"] = sortOrder == SortState.AgeAsc ? SortState.AgeDesc : SortState.AgeAsc;

            if (company != null && company > 0)
            {
                users = users.Where(p => p.CompanyId == company);
            }
            if (!String.IsNullOrEmpty(name))
            {
                users = users.Where(p => p.Name.Contains(name));
            }

            users = sortOrder switch
            {
                SortState.NameDesc => users.OrderByDescending(e => e.Name),
                SortState.AgeAsc => users.OrderBy(e => e.Age),
                SortState.AgeDesc => users.OrderByDescending(e => e.Age),
                _ => users.OrderBy(s => s.Name),
            };

            List<Company> companies = _context.Companies.ToList();
            // устанавливаем начальный элемент, который позволит выбрать всех
            companies.Insert(0, new Company { Name = "¬се", Id = 0 });

            UserListViewModel viewModel = new UserListViewModel
            {
                Users = await users.ToListAsync(),
                Companies = new SelectList(companies, "Id", "Name"),
                Name = name
            };

            return View(viewModel);
        }


        public async Task<IActionResult> Create()
        {
            ViewBag.Companies = await _context.Companies.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                var currentUser = await _context.Users.FirstOrDefaultAsync(p => p.Id == id);
                if (currentUser != null)
                {
                    return View(currentUser);
                }
            }
            return NotFound();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                var currentUser = await _context.Users.FirstOrDefaultAsync(p => p.Id == id);
                if (currentUser != null)
                {
                    return View(currentUser);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                var currentUser = await _context.Users.FirstOrDefaultAsync(p => p.Id == id);
                if (currentUser != null)
                {
                    return View(currentUser);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                var currentUser = await _context.Users.FirstOrDefaultAsync(p => p.Id == id);
                if (currentUser != null)
                {
                    _context.Users.Remove(currentUser);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return NotFound();
        }

        public IActionResult CompanyCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CompanyCreate(Company company)
        {
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Companies()
        {
            return View(await _context.Companies.ToListAsync());
        }
    }

}
