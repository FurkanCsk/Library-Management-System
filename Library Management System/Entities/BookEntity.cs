using System.ComponentModel.DataAnnotations;

namespace Library_Management_System.Entities
{
    public class BookEntity
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Book Title is required")]
        public string Title { get; set; }
        public int AuthorId { get; set; }
        [Required(ErrorMessage = "Book Genre is required")]
        public string Genre { get; set; }
        public DateTime PublishDate { get; set; }
        [Required(ErrorMessage = "Book ISBN is required")]
        public string ISBN { get; set; }
        [Required(ErrorMessage = "Book Copies is required")]
        public int CopiesAvailable { get; set; }
        public string? ImageUrl { get; set; }
        [MinLength(10,ErrorMessage ="Summary must be at least 20 characters long.")]
        public string Summary { get; set; }
        public bool IsDeleted { get; set; }

    }
}
