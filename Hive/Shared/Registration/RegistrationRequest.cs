using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Shared.Registration
{
    public class RegistrationRequest
    {
        [Required(ErrorMessage ="First Name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage ="Last Name is required.")]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [MinLength(1)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Passwords do not match")]
        [Compare(nameof(Password), ErrorMessage = "Password do not match!")]
        public string PasswordConfirm{ get; set; }
    }
}
