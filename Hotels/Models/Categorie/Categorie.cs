namespace Hotels.Models.Categorie
{
    public class Categorie
    {
        public string name { get; set; }
        public ICollection<Hotel_Categorie.Hotel_Categorie> hotel_categorie { get; set; }

    }
}
