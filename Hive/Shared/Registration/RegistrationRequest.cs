using System.ComponentModel.DataAnnotations;

namespace Hive.Shared.Registration
{
    public class RegistrationRequest
    {
        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        [RegularExpression("(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&]?)[A-Za-z\\d@$!%*?&]{8,}", ErrorMessage = "Your password must at least 1 special character, 1 Uppercase character and 1 Digit")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Passwords do not match")]
        [Compare(nameof(Password), ErrorMessage = "Password do not match!")]
        public string PasswordConfirm { get; set; }
    }
}
