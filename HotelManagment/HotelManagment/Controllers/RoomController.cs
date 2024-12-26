using HotelManagment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelManagment.Controllers
{
	public class RoomController : Controller
	{
        HotelContext context = new();
        public IActionResult Index()
        {
            var x = context.Rooms.Where(x => x.Availability).ToList();
            return View(x);
        }
        public IActionResult New()
        {
            return View();
        }
        [HttpPost]
        public IActionResult New(Room room)
        {
            room.Availability = true;
            context.Rooms.Add(room);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult SearchAvailableRooms(DateOnly checkIn, DateOnly checkOut)
        {
            // Fetch available rooms that are not booked within the given range
            var availableRooms = context.Rooms.Where(room =>
                !context.GuestBookRooms.Any(booking =>
                    booking.RoomId == room.RoomId &&
                    ((checkIn >= booking.CheckIn && checkIn < booking.CheckOut) ||
                     (checkOut > booking.CheckIn && checkOut <= booking.CheckOut))
                )).ToList();

            return PartialView("_RoomsListPartial", availableRooms);
        }
    }
}
