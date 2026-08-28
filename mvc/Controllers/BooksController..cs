    using Library_Management_System_mvc.Data;
    using Library_Management_System_mvc.Models;
    using Library_Management_System_mvc.Data;
    using Library_Management_System_mvc.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    namespace Library_Management_System_mvc.Controllers
    {
        public class BooksController : Controller
        {
            private readonly LibraryContext _context;

            public BooksController(LibraryContext context)
            {
                _context = context;
            }

            public async Task<IActionResult> Index(string? search)
            {
                var books = _context.Books.Include(b => b.Category).AsQueryable();

                if (!string.IsNullOrEmpty(search))
                {
                    books = books.Where(b => b.Title.Contains(search) || b.Author.Contains(search));
                }

                return View(await books.ToListAsync());
            }

            public async Task<IActionResult> Details(int? id)
            {
                if (id == null) return NotFound();

                var book = await _context.Books
                    .Include(b => b.Category)
                    .FirstOrDefaultAsync(b => b.Id == id);

                if (book == null) return NotFound();

                return View(book);
            }

            public IActionResult Create()
            {
                ViewBag.CategoryId = new SelectList(_context.Categories, "Id", "Name");
                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create([Bind("Title,Author,ISBN,CategoryId,IsAvailable")] Book book)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(book);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.CategoryId = new SelectList(_context.Categories, "Id", "Name", book.CategoryId);
                return View(book);
            }

            public async Task<IActionResult> Edit(int? id)
            {
                if (id == null) return NotFound();

                var book = await _context.Books.FindAsync(id);
                if (book == null) return NotFound();

                ViewBag.CategoryId = new SelectList(_context.Categories, "Id", "Name", book.CategoryId);
                return View(book);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Author,ISBN,CategoryId,IsAvailable")] Book book)
            {
                if (id != book.Id) return NotFound();

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(book);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!_context.Books.Any(e => e.Id == book.Id)) return NotFound();
                        throw;
                    }
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.CategoryId = new SelectList(_context.Categories, "Id", "Name", book.CategoryId);
                return View(book);
            }

            public async Task<IActionResult> Delete(int? id)
            {
                if (id == null) return NotFound();

                var book = await _context.Books.Include(b => b.Category).FirstOrDefaultAsync(b => b.Id == id);
                if (book == null) return NotFound();

                return View(book);
            }

            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                var book = await _context.Books.FindAsync(id);
                if (book != null)
                {
                    _context.Books.Remove(book);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
        }
    }

