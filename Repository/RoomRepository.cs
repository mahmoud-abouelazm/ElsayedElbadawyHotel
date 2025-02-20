using ElSayedHotel.IRepository;
using ElSayedHotel.Models;
using ElSayedHotel.ViewModel;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        public async Task<bool> AddRoomAsync(RoomViewModel roomViewModel)
        {
            Room room = new Room();
            if(context.Rooms.FirstOrDefault(x=>x.RoomNumber == roomViewModel.RoomNumber) is not null)
            {
                return false;
            }
            IFormFile? imageFile = roomViewModel?.imageFile;
            if (imageFile != null && imageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                Directory.CreateDirectory(uploadsFolder); // Ensure the folder exists

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                string filePath = Path.Combine(uploadsFolder, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                }

                room.ImageName = imageFile.FileName;
                room.ImagePath = "/images/" + fileName;
            }
            room.RoomNumber = roomViewModel.RoomNumber;
            room.Available = true;
            room.Description = roomViewModel.Description;
            room.Price = roomViewModel.Price;
            room.roomType = roomViewModel.Type;
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
