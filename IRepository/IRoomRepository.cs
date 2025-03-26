
using ElSayedHotel.Models;
using ElSayedHotel.ViewModel;

namespace ElSayedHotel.IRepository
{
    public interface IRoomRepository
    {
        void DeleteRoom(int roomNumber);
        List<Room> GetAvailableRooms(RoomSearchViewModel searchRoomModel);
        dynamic GetTypes();
        Task<bool> AddRoomAsync(RoomViewModel roomViewModel);
        bool RoomExist(Guid roomNumber);
    }
}
