namespace ElSayedHotel.ViewModel
{
    public class OwnerPageViewModel
    {
        public int Properties { get; set; }
        public int Clients { get; set; }
        public double Sales { get; set; }
        public int Order { get; set; }
        public List<LastBookingsViewModel> LastBookings { get; set; }
    }
}
