using System.ComponentModel.DataAnnotations.Schema;

namespace ElSayedHotel.Models
{
    public class District
    {
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
        public required int CountryId { get; set; }
        public required int GovernorateId { get; set; }

        [ForeignKey(nameof(CountryId))]
        public virtual Country? DistrictCountry { get; set; }
        [ForeignKey(nameof(GovernorateId))]
        public virtual Governorate? DistrictGovernorate { get; set; }
        public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
    }
}
