using System.ComponentModel.DataAnnotations;

namespace LoginComponent.Models.Request.Auth
{
    public class LoginRequest
    {
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
