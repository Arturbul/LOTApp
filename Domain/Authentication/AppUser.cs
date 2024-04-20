using Core.Models;
using Microsoft.AspNetCore.Identity;

namespace Core.Authentication
{
    public class AppUser : IdentityUser
    {
        public bool FlightsAllowed { get; set; }
        public ICollection<Flight> FlightsModified { get; set; } = null!;
    }
}
