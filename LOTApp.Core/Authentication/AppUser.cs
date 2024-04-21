using Microsoft.AspNetCore.Identity;

namespace LOTApp.Core.Authentication
{
    public class AppUser : IdentityUser
    {
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiry { get; set; }
        public string? Role { get; set; }
    }
}
