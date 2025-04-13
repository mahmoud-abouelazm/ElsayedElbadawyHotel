using Azure.Core;
using ElSayedHotel.IRepository;
using ElSayedHotel.Models;
using ElSayedHotel.ViewModel;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;

namespace ElSayedHotel.Repository
{
    public class RoomRepository : IRoomRepository
    {
        private HotelElsayedContext context ;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly string path;
        public RoomRepository( HotelElsayedContext _context , IWebHostEnvironment webHostEnvironment)
        {
            context = _context;
            this.webHostEnvironment = webHostEnvironment;
            path = webHostEnvironment.WebRootPath+"/rooms/images";
        }
        private async Task<string>AddImageAsync(IFormFile? imageFile)
        {
            string roomImageName = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
            string imagePath = Path.Combine(path, roomImageName);

            // Ensure the directory exists
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return roomImageName;   
        }
        public async Task<bool> AddRoomAsync(RoomViewModel roomViewModel)
        {
            
            Room room = new Room()
            {
                Available = true,
                Description = roomViewModel.Description,
                DistrictId = roomViewModel.DistrictId,
                ownerId = roomViewModel.ownerId,
                Price = roomViewModel.Price,
                ownerRoomName = roomViewModel.roomName,
                capacity = 100,
                Address = "NO address",
                
            };

            IFormFile? imageFile = roomViewModel?.imageFile;
            if (imageFile != null && imageFile.Length > 0)
            {
                var roomImageName = await AddImageAsync(imageFile);
                room.ImageName = imageFile.FileName; 
                room.ImagePath = $"/rooms/images/{roomImageName}";  
            }
            context.Add(room);
            await context.SaveChangesAsync(); // Ensure async call
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
                if(roomSearchViewModel.DistrictId != 0) availableRooms = context.Rooms
                    .Where(r =>
                        !context.Reservations.Any(
                            res => res.RoomId == r.RoomId &&
                            ((checkIn >= res.CheckIn && checkIn < res.CheckOut) ||
                            (checkOut > res.CheckIn && checkOut <= res.CheckOut) ||
                            (checkIn <= res.CheckIn && checkOut >= res.CheckOut)))
                        && r.DistrictId == roomSearchViewModel.DistrictId && r.capacity >= roomSearchViewModel.Guests
                        )
                    .ToList();
                else availableRooms = context.Rooms
                    .Where(r =>
                        !context.Reservations.Any(
                            res => res.RoomId == r.RoomId &&
                            ((checkIn >= res.CheckIn && checkIn < res.CheckOut) ||
                            (checkOut > res.CheckIn && checkOut <= res.CheckOut) ||
                            (checkIn <= res.CheckIn && checkOut >= res.CheckOut)))
                        && r.RoomDistrict.GovernorateId == roomSearchViewModel.GovernorateId && r.capacity >= roomSearchViewModel.Guests
                        ).Include(c=>c.RoomDistrict).ThenInclude(x=>x.DistrictGovernorate)
                    .ToList();

            }
            catch (Exception)
            {
                return null;
            }
            return availableRooms;

        }

        public dynamic GetTypes()
        {
            return context.RoomTypes.Select(x => new {id = x.RoomType1 , type = x.Type}).ToList();
        }

		public bool RoomExist(Guid roomId)
		{
            return context.Rooms.Any(x => x.RoomId == roomId);
		}

        public async Task<List<OwnersRoomListItem>> GetOwnerPropertiesAsync(string ownerId)
        {
            var propertiesList = await context.Rooms
                .Where((room) => room.ownerId == ownerId)
                .Select(room => new OwnersRoomListItem() {
                    Id = room.RoomId,
                    Name = room.ownerRoomName,
                    Price = room.Price,
                    District = room.RoomDistrict.DistrictName
                })
                .AsNoTracking()
                .OrderBy(room => room.District)
                .ToListAsync();
            return propertiesList;
        }
    }
}
