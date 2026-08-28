using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library_Management_System_mvc.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "عنوان الكتاب مطلوب")]
        [StringLength(150)]
        [Display(Name = "العنوان")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "اسم المؤلف مطلوب")]
        [StringLength(100)]
        [Display(Name = "المؤلف")]
        public string Author { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        [Display(Name = "الرقم التسلسلي (ISBN)")]
        public string ISBN { get; set; } = string.Empty;

        [Display(Name = "متاح للاستعارة")]
        public bool IsAvailable { get; set; } = true;

        [Display(Name = "التصنيف")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }

        public List<BorrowRecord>? BorrowRecords { get; set; }
    }
}
