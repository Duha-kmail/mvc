using Library_Management_System_mvc.Data;
using Library_Management_System_mvc.Models;
using Library_Management_System_mvc.Data;
using Library_Management_System_mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System_mvc.Controllers
{
    public class BorrowController : Controller
    {
        private readonly LibraryContext _context;
        public BorrowController(LibraryContext context) => _context = context;

        public async Task<IActionResult> Index()
        {
            var records = await _context.BorrowRecords
                .Include(r => r.Book)
                .Include(r => r.Member)
                .OrderByDescending(r => r.BorrowDate)
                .ToListAsync();
            return View(records);
        }

        public IActionResult Create()
        {
            ViewBag.BookId = new SelectList(_context.Books.Where(b => b.IsAvailable), "Id", "Title");
            ViewBag.MemberId = new SelectList(_context.Members, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookId,MemberId")] BorrowRecord record)
        {
            var book = await _context.Books.FindAsync(record.BookId);

            if (book == null || !book.IsAvailable)
            {
                ModelState.AddModelError("", "الكتاب المختار غير متاح للاستعارة حالياً");
            }

            if (ModelState.IsValid)
            {
                record.BorrowDate = DateTime.Now;
                _context.BorrowRecords.Add(record);

                book!.IsAvailable = false;
                _context.Books.Update(book);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.BookId = new SelectList(_context.Books.Where(b => b.IsAvailable), "Id", "Title", record.BookId);
            ViewBag.MemberId = new SelectList(_context.Members, "Id", "Name", record.MemberId);
            return View(record);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Return(int id)
        {
            var record = await _context.BorrowRecords.Include(r => r.Book).FirstOrDefaultAsync(r => r.Id == id);
            if (record == null) return NotFound();

            record.ReturnDate = DateTime.Now;
            if (record.Book != null) record.Book.IsAvailable = true;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}