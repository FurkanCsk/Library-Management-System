using System.ComponentModel.DataAnnotations;

namespace Library_Management_System.Entities
{
    public class AuthorEntity
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Author First Name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Author Last Name is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Author DateTime is required")]
        public DateTime DateOfBirth { get; set; }
        public string? ImageUrl { get; set; }
        [MinLength(10, ErrorMessage = "Bio must be at least 20 characters long.")]
        public string Bio { get; set; }
        public bool IsDeleted { get; set; }
    }
}
