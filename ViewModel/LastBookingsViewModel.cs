namespace ElSayedHotel.ViewModel
{
    public class LastBookingsViewModel
    {
        public string RoomName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double Amount { get; set; }
        private string statusColor;// 0 pending 1 Confirmed 2 canceled 3 Completed
        public string StatusColor { get { return statusColor; } set{ 
                switch (value) {
                    case "Pending":
                        statusColor = "warning"; 
                        break;
                    case "Confirmed":
                        statusColor = "success";
                        break;
                    case "Canceled":
                        statusColor = "danger";
                        break;
                    case "Completed":
                        statusColor = "primary";
                        break;
                    default:
                        statusColor = "danger";
                        break;
                } 
            }
        }
        public string Status { get; set; }
    }
}
