using HotelManagment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Controllers
{
    public class BookController : Controller
    {
        HotelContext context = new();
        public IActionResult Index()
        {
            return View();
        }
       
        public IActionResult book(int roomId)
        {
            var guests = context.Guests.ToList();
            ViewBag.Guests = guests;
            ViewBag.RoomId = roomId;
            ViewBag.RoomNum = context.Rooms.Where(x=>x.RoomId == roomId).Select(x=>x.RoomNum);
            return View();
        }
        [HttpPost]
        public IActionResult book(int roomId, int guestId, DateOnly checkIn, DateOnly checkOut)
        {
            if (checkOut <= checkIn)
            {
                TempData["Error"] = "Check-Out date must be later than Check-In date.";
                return RedirectToAction("book");
            }

            // Check if the room is already booked
            if (context.GuestBookRooms.Any(b => b.RoomId == roomId && b.CheckOut > checkIn))
            {
                TempData["Error"] = "Room is already booked for the selected dates.";
                return RedirectToAction("Index");
            }
            Reservation rev = new();
            context.Add(rev);
            context.SaveChanges();
            // Create the booking record
            var booking = new GuestBookRoom
            {
                BookingId = rev.BookingId,
                GuestId = guestId,
                RoomId = roomId,
                CheckIn = checkIn,
                CheckOut = checkOut
            };
            Room room = context.Rooms.Where(x => x.RoomId == roomId).First();
            room.Availability = false;
            context.Update(room);
            context.GuestBookRooms.Add(booking);
            context.SaveChanges();

            TempData["Success"] = $"Room {roomId} booked successfully for Guest ID {guestId}.";
            return RedirectToAction("Index" , "room");

    }
}
}
