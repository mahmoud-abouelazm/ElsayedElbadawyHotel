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

			string? roomId = value.ToString();
			bool exists = dbContext.Rooms.Any(x => x.RoomId.ToString() == roomId);
			return exists ? new ValidationResult(ErrorMessage ?? "Room number already exists.")
				: ValidationResult.Success;
		}
	}
}
