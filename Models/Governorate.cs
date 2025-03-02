using System.ComponentModel.DataAnnotations.Schema;

namespace ElSayedHotel.Models
{
    public class Governorate
    {
        public int GovernorateId { get; set; }
        public int CountryId { get; set; }
        [ForeignKey(nameof(CountryId))]
        public virtual Country? GovernorateCountry { get; set; }
        public virtual List<District>? Districts { get; set; }
        public required string GovernorateName { get; set; }
    }
}
