using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library_Management_System_mvc.Models
{
    public class BorrowRecord
    {
        public int Id { get; set; }

        [Display(Name = "الكتاب")]
        public int BookId { get; set; }

        [ForeignKey("BookId")]
        public Book? Book { get; set; }

        [Display(Name = "العضو")]
        public int MemberId { get; set; }

        [ForeignKey("MemberId")]
        public Member? Member { get; set; }

        [Display(Name = "تاريخ الاستعارة")]
        [DataType(DataType.Date)]
        public DateTime BorrowDate { get; set; } = DateTime.Now;

        [Display(Name = "تاريخ الإرجاع")]
        [DataType(DataType.Date)]
        public DateTime? ReturnDate { get; set; }
    }
}
