using System.ComponentModel.DataAnnotations;

namespace Library_Management_System_mvc.Models
{
    public class Member
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "اسم العضو مطلوب")]
        [StringLength(100)]
        [Display(Name = "الاسم")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress(ErrorMessage = "بريد إلكتروني غير صحيح")]
        [Display(Name = "البريد الإلكتروني")]
        public string Email { get; set; } = string.Empty;

        [Phone(ErrorMessage = "رقم هاتف غير صحيح")]
        [Display(Name = "رقم الهاتف")]
        public string? Phone { get; set; }

        public List<BorrowRecord>? BorrowRecords { get; set; }
    }
}
