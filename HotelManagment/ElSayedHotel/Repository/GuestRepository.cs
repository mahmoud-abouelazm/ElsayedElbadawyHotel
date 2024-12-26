using ElSayedHotel.IRepository;
using ElSayedHotel.Models;

namespace ElSayedHotel.Repository
{
    public class GuestRepository : IGuestRepository
    {
        public HotelElsayedContext context = new();
    }
}
