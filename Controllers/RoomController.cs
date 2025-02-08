using ElSayedHotel.IRepository;
using ElSayedHotel.Models;
using ElSayedHotel.Repository;
using ElSayedHotel.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ElSayedHotel.Controllers
{
    public class RoomController : Controller
    {
        private IRoomRepository roomRepository;
        public RoomController(IRoomRepository _roomRepository)
        {
            roomRepository = _roomRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult newRoom()
        {
            RoomViewModel roomViewModel = new RoomViewModel() { roomTypesList = roomRepository.GetTypes()};
            return View(roomViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult newRoom(RoomViewModel room)
        {
            if(ModelState.IsValid && roomRepository.AddRoom(room))
            {
                return RedirectToAction("index", "home");
            }
            return View(room);
        }
    }
}
