using Hotels.Models.Base;

namespace Hotels.Models.Review
{
    public class Review:BaseEntity
    {
        public String descriere { get; set; }
        public int nota { get; set; }
        public Hotel.Hotel hotel { get; set; }

    }
}
