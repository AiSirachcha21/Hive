using System.ComponentModel.DataAnnotations;

namespace Hive.Shared.Login
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Email is required.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
