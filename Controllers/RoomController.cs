using ElSayedHotel.Filters;
using ElSayedHotel.IRepository;
using ElSayedHotel.Models;
using ElSayedHotel.Repository;
using ElSayedHotel.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElSayedHotel.Controllers
{
    [Authorize]
    public class RoomController : Controller
    {
        private IRoomRepository roomRepository;
        public RoomController(IRoomRepository _roomRepository)
        {
            roomRepository = _roomRepository;
        }
        [Authorize(Roles = "admin")]
        public IActionResult newRoom()
        {
            RoomViewModel roomViewModel = new RoomViewModel() { roomTypesList = roomRepository.GetTypes()};
            return View(roomViewModel);
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> newRoom(RoomViewModel room)
        {
            if (await roomRepository.AddRoomAsync(room) && ModelState.IsValid )
            {
                return  RedirectToAction("index", "home");
            }
            return View(room);
        }
    }
}
