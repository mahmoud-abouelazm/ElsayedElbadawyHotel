using ElSayedHotel.IRepository;
using ElSayedHotel.Models;
using Microsoft.EntityFrameworkCore;

namespace ElSayedHotel.Repository
{
    public class RoomRepository : IRoomRepository
    {
        HotelElsayedContext context = new();
        public void DeleteRoom(int roomNumber)
        {
            throw new NotImplementedException();
        }


        public List<Room> GetAvailableRooms(DateTime checkIn, DateTime? checkOut)
        {
            // Query to get rooms that are available within the given date range
            var availableRooms = context.Rooms
                .Where(r => !context.Reservations
                    .Any(res => res.RoomNumber == r.RoomNumber &&
                        ((checkIn >= res.CheckIn && checkIn < res.CheckOut) ||
                        (checkOut > res.CheckIn && checkOut <= res.CheckOut)||
                        (checkIn <= res.CheckIn && checkOut >= res.CheckOut)
                        ))
                )
                .ToList();

            return availableRooms;
        }

        public List<RoomType> GetTypes()
        {
            return context.RoomTypes.ToList();
        }
    }
}
