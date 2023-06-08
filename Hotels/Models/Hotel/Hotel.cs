using Hotels.Models.Base;
using Hotels.Models.Categorie;
using Hotels.Models.Hotel_Categorie;
using Hotels.Models.Review;

namespace Hotels.Models.Hotel
{
    public class Hotel:BaseEntity
    {
        public String name { get; set; }
        public String city { get; set; }
        public ICollection<Review.Review> reviews { get; set; }
        public ICollection<Hotel_Categorie.Hotel_Categorie> hotel_categories { get; set; }

    }
}
