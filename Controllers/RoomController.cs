using ElSayedHotel.Filters;
using ElSayedHotel.IRepository;
using ElSayedHotel.Models;
using ElSayedHotel.Repository;
using ElSayedHotel.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElSayedHotel.Controllers
{
    [Authorize]
    public class RoomController : Controller
    {
        private IRoomRepository roomRepository;
        private readonly HotelElsayedContext context;

        public RoomController(IRoomRepository _roomRepository , HotelElsayedContext context)
        {
            roomRepository = _roomRepository;
            this.context = context;
        }
        public IActionResult newRoom()
        {
            RoomViewModel roomViewModel = new RoomViewModel() { roomTypesList = roomRepository.GetTypes()};
            return View(roomViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> newRoom(RoomViewModel room)
        {
            if (await roomRepository.AddRoomAsync(room) && ModelState.IsValid )
            {
                return  RedirectToAction("index", "home");
            }
            return View(room);
        }
        [HttpPost]
        [AllowAnonymous]
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
