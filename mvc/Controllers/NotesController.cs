using Library_Management_System_mvc.Data;
using Library_Management_System_mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System_mvc.Controllers
{
    public class NotesController : Controller
    {
        private readonly LibraryContext _context;

        public NotesController(LibraryContext context)
        {
            _context = context;
        }

        // Returns partial list of notes (most recent first)
        public async Task<IActionResult> List()
        {
            var notes = await _context.Notes.OrderByDescending(n => n.CreatedAt).ToListAsync();
            return PartialView("_NotesList", notes);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return BadRequest("Content is required");
            }

            var note = new Note
            {
                Content = content.Trim(),
                CreatedAt = DateTime.UtcNow
            };

            _context.Notes.Add(note);
            await _context.SaveChangesAsync();

            return Json(new { success = true, id = note.Id, content = note.Content, createdAt = note.CreatedAt });
        }
    }
}
