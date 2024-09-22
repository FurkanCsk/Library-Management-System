using System.ComponentModel.DataAnnotations;

namespace Library_Management_System.ViewModels
{
    public class AuthViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        [Compare(nameof(Password),ErrorMessage ="Password is can not be different.")]
        public string PasswordConfirm { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime JoinDate { get; set; }
    }
}
