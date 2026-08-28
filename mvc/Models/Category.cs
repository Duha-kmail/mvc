using System.ComponentModel.DataAnnotations;
namespace Library_Management_System_mvc.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "اسم التصنيف مطلوب")]
        [StringLength(50)]
        [Display(Name = "اسم التصنيف")]
        public string Name { get; set; } = string.Empty;

        public List<Book>? Books { get; set; }
    }
}
