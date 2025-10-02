using app_hw.Data;
using app_hw.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace app_hw.Controllers
{
    [Authorize]
    public class NotesController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public NotesController(ApplicationContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var notes = await _context.Notes.Where(n => n.UserId == user.Id).ToListAsync();
            return View(notes);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Note note)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                note.UserId = user.Id;
                _context.Notes.Add(note);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(note);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var note = await _context.Notes.FindAsync(id);
            if (note == null || note.UserId != _userManager.GetUserId(User)) return NotFound();
            return View(note);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Note note)
        {
            if (ModelState.IsValid)
            {
                _context.Update(note);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(note);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var note = await _context.Notes.FindAsync(id);
            if (note == null || note.UserId != _userManager.GetUserId(User)) return NotFound();

            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
