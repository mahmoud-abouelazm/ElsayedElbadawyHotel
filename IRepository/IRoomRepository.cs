
using ElSayedHotel.Models;
using ElSayedHotel.ViewModel;

namespace ElSayedHotel.IRepository
{
    public interface IRoomRepository
    {
        void DeleteRoom(int roomNumber);
        List<Room> GetAvailableRooms(DateTime checkInDate, DateTime? checkOutDate);
        List<RoomType> GetTypes();
        Task<bool> AddRoomAsync(RoomViewModel roomViewModel);
        bool RoomExist(int roomNumber);
    }
}
