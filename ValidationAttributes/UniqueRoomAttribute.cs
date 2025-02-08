using ElSayedHotel.Models;
using ElSayedHotel.Repository;
using System.ComponentModel.DataAnnotations;

namespace ElSayedHotel.ValidationAttributes
{
	public class UniqueRoomAttribute:ValidationAttribute
	{
		protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
		{
			// Get the DbContext from the DI container
			var dbContext = validationContext.GetRequiredService<HotelElsayedContext> ();

			int roomNumber = (int)value;
			bool exists = dbContext.Rooms.Any(x => x.RoomNumber == roomNumber);

			return exists ? new ValidationResult(ErrorMessage ?? "Room number already exists.")
				: ValidationResult.Success;
		}
	}
}
