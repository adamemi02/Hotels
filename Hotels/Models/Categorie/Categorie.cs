using Hotels.Models.Base;

namespace Hotels.Models.Categorie
{
    public class Categorie:BaseEntity
    {
        public string name { get; set; }
        public ICollection<Hotel_Categorie.Hotel_Categorie> hotel_categorie { get; set; }

    }
}
