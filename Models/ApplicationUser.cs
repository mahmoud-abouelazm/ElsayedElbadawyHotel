using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElSayedHotel.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
        public string? Address { get; set; }
        [InverseProperty("Guest")]
        public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();

        [InverseProperty("Guest")]
        public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>(); 
        [InverseProperty("owner")]
        public virtual ICollection<Room> ownedRooms { get; set; } = new List<Room>();
    }
}
