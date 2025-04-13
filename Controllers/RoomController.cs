using ElSayedHotel.IRepository;
using ElSayedHotel.Models;
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
        [Authorize(Roles ="Owner")]
        public IActionResult AddRoom()
        {
            RoomViewModel roomViewModel = new RoomViewModel();
            return View(roomViewModel);
        }
        [HttpPost]
        [Authorize(Roles = "Owner")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRoom(RoomViewModel room)
        {

            if (ModelState.IsValid && await roomRepository.AddRoomAsync(room))
            {
                return RedirectToAction("index", "home");
            }
            return View(room);
        }
        [HttpPost]
        [Authorize]
        public IActionResult GetAvailableRooms([FromBody] RoomSearchViewModel request)
        {
            if (ModelState.IsValid)
            {
                request.CheckIn = new DateTime(request.CheckIn.Year, request.CheckIn.Month, request.CheckIn.Day, 14, 0, 0); // 14:00
                request.CheckOut = new DateTime(request.CheckOut.Year, request.CheckOut.Month, request.CheckOut.Day, 9, 0, 0); // 09:00
                var res = roomRepository.GetAvailableRooms(request);
                if (res is null) return Json(null);
                var x = from room in res
                        select new { imageUrl = room.ImagePath , price = room.Price , name = $"{room.RoomDistrict.DistrictName} , {room.RoomDistrict.DistrictGovernorate.GovernorateName}" };
                
                return Json(x);
            }
            else
            {
                return Json(null);
            }
        }
        
    }
}
