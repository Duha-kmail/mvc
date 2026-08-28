using Library_Management_System_mvc.Data;
using Library_Management_System_mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Library_Management_System_mvc.Models;
using System.Diagnostics;

namespace Library_Management_System_mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly LibraryContext _context;
        public HomeController(LibraryContext context) => _context = context;

        public async Task<IActionResult> Index()
        {
            ViewBag.BooksCount = await _context.Books.CountAsync();
            ViewBag.MembersCount = await _context.Members.CountAsync();
            ViewBag.BorrowedCount = await _context.BorrowRecords.CountAsync(r => r.ReturnDate == null);
            return View();
        }

        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}