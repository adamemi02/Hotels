using Hotels.Models.Base;
using System.Text.Json.Serialization;

namespace Hotels.Models.Review
{
    public class Review:BaseEntity
    {
        public String descriere { get; set; } = null!;
        public int nota { get; set; } = 0;
        [JsonIgnore]
        public Guid HotelId { get; set; }
        public virtual Hotel.Hotel hotel { get; set; }

    }
}
