using ElSayedHotel.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ElSayedHotel.ValidationAttributes;
namespace ElSayedHotel.ViewModel
{
    public class RoomViewModel
    {
        
        [Required]
        [UniqueRoom]
        public Guid RoomId { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        [Range(10, double.MaxValue, ErrorMessage = "The price must be at least 10.")] public double Price { get; set; }
        [StringLength(255)]
        [Required]
        public string roomName { get; set; }
        [StringLength(255)]
        public string? Description { get; set; }
        [Required]
        [Range(1 , int.MaxValue , ErrorMessage ="You should select a type")] 
        public int Type { get; set; }
        public List<RoomType> roomTypesList { get; set; }

        public IFormFile? imageFile {get; set;}
    }
}
