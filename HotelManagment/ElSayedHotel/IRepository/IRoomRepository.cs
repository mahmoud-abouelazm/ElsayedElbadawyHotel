
using ElSayedHotel.Models;

namespace ElSayedHotel.IRepository
{
    public interface IRoomRepository
    {
        void DeleteRoom(int roomNumber);
        List<Room> GetAvailableRooms(DateTime checkInDate, DateTime? checkOutDate);
        List<RoomType> GetTypes();
    }
}
