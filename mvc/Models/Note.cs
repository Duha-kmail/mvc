using System.ComponentModel.DataAnnotations;

namespace Library_Management_System_mvc.Models
{
    public class Note
    {
        public int Id { get; set; }

        [Required]
        [StringLength(1000)]
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
