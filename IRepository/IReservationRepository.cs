using ElSayedHotel.Models;

namespace ElSayedHotel.IRepository
{
    public interface IReservationRepository
    {
        void CreateReservation(Reservation reservation);
    }
}
