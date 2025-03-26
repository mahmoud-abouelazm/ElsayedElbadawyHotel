using ElSayedHotel.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ElSayedHotel.ValidationAttributes;

namespace ElSayedHotel.ViewModel
{
    public class RoomViewModel
    {
        [StringLength(255)]
        [Required]
        public string Address { get; set; }
        [Required]
        public int capacity { get; set; }
        [Required]
        public bool Available { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        [Range(10, double.MaxValue, ErrorMessage = "The price must be at least 10.")]
        public double Price { get; set; }

        [StringLength(255)]
        [Required]
        [Display(Name = "Property Name")]
        public string roomName { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        public IFormFile? imageFile { get; set; }

        // New fields for Governorate and District
        [Required(ErrorMessage = "Governorate selection is required")]
        public int GovernorateId { get; set; }

        [Required(ErrorMessage = "District selection is required")]
        public int DistrictId { get; set; }

        public string ownerId { get; set; }
    }
}
