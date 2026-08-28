using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library_Management_System_mvc.Views.Borrow
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public int? BookId { get; set; }

        [BindProperty]
        public int? MemberId { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            // Minimal post handler to allow form submission.
            // Real implementation should validate and save the borrow record.
            return RedirectToPage("./Index");
        }
    }
}
