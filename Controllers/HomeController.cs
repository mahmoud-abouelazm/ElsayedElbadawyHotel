using ElSayedHotel.Filters;
using ElSayedHotel.IRepository;
using ElSayedHotel.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NuGet.Protocol;
using System.Diagnostics;

namespace ElSayedHotel.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IReservationRepository _reservationRepository;

        public HomeController(IRoomRepository roomRepository, IReservationRepository reservationRepository)
        {
            _roomRepository = roomRepository;
            
            _reservationRepository = reservationRepository;
        }
        public IActionResult data(RoomType r , string type)
        {
            return Content(type);
        }
        public IActionResult Index(string error = "Hotel Management System")
        {
            return View((object)error);
        }
        [HttpPost]
        public IActionResult SearchAvailableRooms(DateTime checkInDate, DateTime checkOutDate)
        {
            if (checkInDate == null || checkOutDate == null)
            {
                return null;
            }
            var checkInDateTime = new DateTime(checkInDate.Year, checkInDate.Month, checkInDate.Day, 14, 0, 0); // 14:00
            var checkOutDateTime = new DateTime(checkOutDate.Year, checkOutDate.Month, checkOutDate.Day, 9, 0, 0); // 09:00
            
            var availableRooms =  _roomRepository.GetAvailableRooms(checkInDateTime, checkOutDateTime);
            ViewBag.type = _roomRepository.GetTypes();
            return PartialView("_AvailableRooms", availableRooms);
        }

        [HttpPost]
        public ActionResult BookRoom(int roomNumber, string guestId, DateTime checkInDate, DateTime? checkOutDate)
        {
            var checkInDateTime = new DateTime(checkInDate.Year, checkInDate.Month, checkInDate.Day, 14, 0, 0); // 14:00
            var checkOutDateTime = new DateTime(checkOutDate.Value.Year, checkOutDate.Value.Month, checkOutDate.Value.Day, 9, 0, 0); // 09:00

            var reservation = new Reservation
            {
                RoomNumber = roomNumber,
                GuestId = guestId,
                CheckIn = checkInDateTime,
                CheckOut = checkOutDateTime
             
            };

            _reservationRepository.CreateReservation(reservation);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteRoom(int roomNumber)
        {
            _roomRepository.DeleteRoom(roomNumber);
            
            return RedirectToAction("Index");
        }
    }


}
