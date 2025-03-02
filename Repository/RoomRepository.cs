using Azure.Core;
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
            if(context.Rooms.FirstOrDefault(x=>x.RoomId == roomViewModel.RoomId) is not null)
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
            room.RoomId = roomViewModel.RoomId;
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


        public List<Room>? GetAvailableRooms(RoomSearchViewModel roomSearchViewModel)
        {
            List<Room>? availableRooms = null;
            var checkIn = roomSearchViewModel.CheckIn;
            var checkOut = roomSearchViewModel.CheckOut;
            if (checkOut < checkIn) return null;
            try
            {
                availableRooms = context.Rooms
                    .Where(r =>
                        !context.Reservations.Any(
                            res => res.RoomId == r.RoomId &&
                            ((checkIn >= res.CheckIn && checkIn < res.CheckOut) ||
                            (checkOut > res.CheckIn && checkOut <= res.CheckOut) ||
                            (checkIn <= res.CheckIn && checkOut >= res.CheckOut)))
                        && r.DistrictId == roomSearchViewModel.DistrictId && r.capacity >= roomSearchViewModel.Guests
                        )
                    .ToList();
            }
            catch (Exception)
            {
                return null;
            }
            return availableRooms;

        }

        public List<RoomType> GetTypes()
        {
            return context.RoomTypes.ToList();
        }

		public bool RoomExist(Guid roomId)
		{
            return context.Rooms.Any(x => x.RoomId == roomId);
		}
	}
}
