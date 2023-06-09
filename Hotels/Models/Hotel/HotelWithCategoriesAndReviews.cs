namespace Hotels.Models.Hotel
{
    public class HotelWithCategoriesAndReviews
    {
        public String name { get; set; }
        public String city { get; set; }
        public ICollection<Review.Review> Reviews= new List<Review.Review>();
        public ICollection<Categorie.Categorie> Categories= new List<Categorie.Categorie>();
    }
}
