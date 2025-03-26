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
        private readonly HotelElsayedContext context;
        private readonly IRoomRepository _roomRepository;
        private readonly IReservationRepository _reservationRepository;

        public HomeController(HotelElsayedContext context ,IRoomRepository roomRepository, IReservationRepository reservationRepository)
        {
            this.context = context;
            _roomRepository = roomRepository;
            
            _reservationRepository = reservationRepository;
        }
        public IActionResult data(RoomType r , string type)
        {
            return Content(type);
        }
        public IActionResult Index()
        {
            if(User.IsInRole("Owner"))return View("owner");
            return View();
        }


        [HttpPost]
        public ActionResult BookRoom(Guid roomId, string guestId, DateTime checkInDate, DateTime? checkOutDate)
        {
            var checkInDateTime = new DateTime(checkInDate.Year, checkInDate.Month, checkInDate.Day, 14, 0, 0); // 14:00
            var checkOutDateTime = new DateTime(checkOutDate.Value.Year, checkOutDate.Value.Month, checkOutDate.Value.Day, 9, 0, 0); // 09:00

            var reservation = new Reservation
            {
                RoomId = roomId,
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

        public IActionResult GetGovernorates()
        {
            var x = context.Set<Governorate>()
                .Select(x=> new {id = x.GovernorateId , name = x.GovernorateName}).ToList();
            return Json(x);

        }
        
        public IActionResult GetDistricts(int governorateId) // /Home/GetDistricts?governorateId=$
        {
            var x = context.Set<District>().Where(x=>x.GovernorateId == governorateId)
                .Select(x=> new {id = x.DistrictId , name = x.DistrictName}).ToList();
            return Json(x);

        }
    }


}
