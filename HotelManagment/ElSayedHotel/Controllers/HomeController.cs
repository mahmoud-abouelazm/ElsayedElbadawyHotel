using ElSayedHotel.IRepository;
using ElSayedHotel.Models;
using Microsoft.AspNetCore.Mvc;
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

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SearchAvailableRooms(DateTime checkInDate, DateTime? checkOutDate)
        {
            // Set the fixed check-in and check-out times
            var checkInDateTime = new DateTime(checkInDate.Year, checkInDate.Month, checkInDate.Day, 14, 0, 0); // 14:00
            var checkOutDateTime = new DateTime(checkOutDate.Value.Year, checkOutDate.Value.Month, checkOutDate.Value.Day, 9, 0, 0); // 09:00

            var availableRooms = _roomRepository.GetAvailableRooms(checkInDateTime, checkOutDateTime);
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
                CheckOut = checkOutDateTime,
                Active = false
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
