using ElSayedHotel.Filters;
using ElSayedHotel.IRepository;
using ElSayedHotel.Models;
using ElSayedHotel.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NuGet.Protocol;
using System.Diagnostics;
using System.Security.Claims;

namespace ElSayedHotel.Controllers
{
    
    public class HomeController : Controller
    {
        public static string GetStatus(int status)
        {
            switch (status) // Handle nulls safely
            {
                case 0:
                    return "Pending";
                case 1:
                    return "Confirmed";
                case 2:
                    return "Canceled";
                case 3:
                    return "Completed";
                default:
                    return "Unknown or Null Status";
            }

        }
        private readonly HotelElsayedContext context;
        private readonly IRoomRepository _roomRepository;
        private readonly IReservationRepository _reservationRepository;

        public HomeController(HotelElsayedContext context ,  IRoomRepository roomRepository, IReservationRepository reservationRepository )
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
            if (User.IsInRole("Owner"))
            {
                var ownerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var ownerReservations = context.Reservations
                    .Where(x => x.OwnerId == ownerId).OrderByDescending(x => x.CheckIn).ToList();
                var ownerProperties = context.Rooms.Where(x => x.ownerId == ownerId).ToList();
                var ownerPropertiesCount = ownerProperties.Count();
                var data = new OwnerPageViewModel()
                {
                    Clients = ownerReservations
                    .Select(x => x.GuestId)
                    .Distinct().Count(),
                    Order = ownerReservations.Count(),
                    Properties = ownerPropertiesCount,
                    Sales = (double)ownerReservations.Where(x=>x.Status == 3).Select(x => x.Total).Sum(x => x),
                    LastBookings = ownerReservations.Select(x=>new LastBookingsViewModel()
                    {
                        Amount = (double)x.Total,
                        EndDate = (DateTime)x.CheckOut,
                        RoomName = x.RoomNavigation.ownerRoomName,
                        StartDate = x.CheckIn,
                        Status = GetStatus((int)x.Status),
                        StatusColor = GetStatus((int)x.Status)
                    }).ToList()
                };
                return View("owner" , data);
            }
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
