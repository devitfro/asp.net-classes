using Microsoft.AspNetCore.Mvc;
using app_hw.Models;
using Microsoft.EntityFrameworkCore;

namespace app_hw.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _appdb;

        public ProductController(AppDbContext appdb)
        {
            _appdb = appdb;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _appdb.Products.ToListAsync();
            return Json(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product pr)
        {
            if (pr == null)
            {
                return View("Create");
            }
            await _appdb.Products.AddAsync(pr);
            await _appdb.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public IActionResult Details()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Details(int id)
        {
            var pr = await _appdb.Products.FindAsync(id);
            if (pr == null) return NotFound();

            return Json(pr);
        }

        public IActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id) 
        {

            var pr = await _appdb.Products.FindAsync(id);
            if (pr == null) return NotFound();

            _appdb.Products.Remove(pr);
            await _appdb.SaveChangesAsync();

            return Json(pr);
        }

        [HttpGet]
        public async Task<IActionResult> Search(string keyword)
        {
            var result = await _appdb.Products
                .Where(p => !string.IsNullOrEmpty(p.Name) && p.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();

            return Json(result);
        }
    }
}
