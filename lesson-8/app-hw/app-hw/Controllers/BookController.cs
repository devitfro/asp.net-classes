using app_hw.Models;
using app_hw.Services;
using app_hw.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace app_hw.Controllers
{
    public class BookController : Controller
    {
        public BookService _bs;

        public BookController(BookService bookService)
        {
            _bs = bookService;
        }

        public IActionResult Index()
        {
            var books = GetBooks();
            return View(books);
        }

        public List<BookViewModel> GetBooks()
        {
            List<BookViewModel> bookModels = _bs.GetBooks()
                .Select(b => new BookViewModel
                {
                    Title = b.Title,
                    Author = b.Author,
                    Genre = b.Genre,
                    Year = b.Year
                })
                .ToList();

            return bookModels;
        }

        public IActionResult AddBook()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddBook(BookViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var newBook = new Book
            {
                Id = _bs.GetBooks().Max(b => b.Id) + 1,
                Title = model.Title,
                Author = model.Author,
                Genre = model.Genre,
                Year = model.Year
            };

            _bs.AddBook(newBook);
            return RedirectToAction("Index");
        }
    }
}
