using Core.Models;
using Microsoft.AspNetCore.Identity;

namespace Core.Authentication
{
    public class AppUser : IdentityUser
    {
        public ICollection<Flight> FlightsModified { get; set; } = null!;
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiry { get; set; }
        public string? Role { get; set; }
    }
}
