using System.ComponentModel.DataAnnotations;

namespace ElSayedHotel.ViewModel
{
    public class RoomSearchViewModel
    {
        [Required]
        public int GovernorateId { get; set; }
        [Required]

        public int DistrictId { get; set; }
        [Required]

        public DateTime CheckIn { get; set; }
        [Required]

        public DateTime CheckOut { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Number of guests must be at least 1.")]
        public int Guests { get; set; }
    }
}
