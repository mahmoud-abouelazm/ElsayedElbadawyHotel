using ElSayedHotel.IRepository;
using ElSayedHotel.Models;
using ElSayedHotel.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Collections.Generic;
using System.Diagnostics;

namespace ElSayedHotel.Repository
{
    public class RoomRepository : IRoomRepository
    {
        private HotelElsayedContext context ;
        public RoomRepository( HotelElsayedContext _context )
        {
            context = _context;
        }

        public bool AddRoom(RoomViewModel roomViewModel)
        {
            Room room = new Room();
            if(context.Rooms.FirstOrDefault(x=>x.RoomNumber == roomViewModel.RoomNumber) is not null)
            {
                return false;
            }
            room.RoomNumber = roomViewModel.RoomNumber;
            room.Available = true;
            room.Description = roomViewModel.Description;
            room.Price = roomViewModel.Price;
            room.Type = roomViewModel.Type;
            context.Add(room);
            context.SaveChanges();
            return true;
        }

        public void DeleteRoom(int roomNumber)
        {
            throw new NotImplementedException();
        }


        public List<Room> GetAvailableRooms(DateTime checkIn, DateTime? checkOut)
        {
            var availableRooms = new List<Room>();
            try
            {
                availableRooms = context.Rooms
                    .Where(r =>
                        !context.Reservations.Any(
                            res => res.RoomNumber == r.RoomNumber &&
                            ((checkIn >= res.CheckIn && checkIn < res.CheckOut) ||
                            (checkOut > res.CheckIn && checkOut <= res.CheckOut) ||
                            (checkIn <= res.CheckIn && checkOut >= res.CheckOut)
                        ))
                        )
                    .ToList();
            }
            catch (Exception)
            {
                throw(new Exception("There is a problem with GetAvailableRooms"));
            }
            return availableRooms;
        }

        public List<RoomType> GetTypes()
        {
            return context.RoomTypes.ToList();
        }

		public bool RoomExist(int roomNumber)
		{
            return context.Rooms.Any(x => x.RoomNumber == roomNumber);
		}
	}
}
