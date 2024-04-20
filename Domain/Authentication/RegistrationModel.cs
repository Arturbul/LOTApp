using System.ComponentModel.DataAnnotations;

namespace Core.Authentication
{
    public class RegistrationModel
    {
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        [EmailAddress]
        public string Email { get; set; } = null!;

        public string? Role { get; set; } //Testing purposes
    }
}
