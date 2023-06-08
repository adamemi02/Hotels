namespace Hotels.Models.Review
{
    public class Review_Request
    {
        public Guid ReviewId { get; set; }
        public Guid idHotel { get; set; }
        public int nota { get; set; }
        public string descriere { get; set; }

    }
}
