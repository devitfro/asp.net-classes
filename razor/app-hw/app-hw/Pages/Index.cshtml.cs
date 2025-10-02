using app_hw.Data;
using app_hw.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;

namespace app_hw.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationContext _db;

        public IndexModel(ApplicationContext db) => _db = db;

        public List<TodoItem> Todos { get; set; } = new();

        [BindProperty]
        public TodoItem NewTodo { get; set; } = new();

        public async Task OnGetAsync()
        {
            Todos = await _db.Todos.ToListAsync();
        }

        public async Task<IActionResult> OnPostAddAsync()
        {
            if (!string.IsNullOrWhiteSpace(NewTodo.Title))
            {
                _db.Todos.Add(NewTodo);
                await _db.SaveChangesAsync();
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var todo = await _db.Todos.FindAsync(id);
            if (todo != null)
            {
                _db.Todos.Remove(todo);
                await _db.SaveChangesAsync();
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostToggleAsync(int id)
        {
            var todo = await _db.Todos.FindAsync(id);
            if (todo != null)
            {
                todo.IsDone = !todo.IsDone;
                await _db.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}
