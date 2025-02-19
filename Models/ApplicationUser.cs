using Microsoft.AspNetCore.Identity;

namespace ElSayedHotel.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Address { get; set; }
    }
}
