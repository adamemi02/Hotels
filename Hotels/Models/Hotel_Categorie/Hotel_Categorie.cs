namespace Hotels.Models.Hotel_Categorie
{
    public class Hotel_Categorie
    {
        public Guid HotelId { get; set; }
        public Guid CategorieId { get; set; }
        public Hotel.Hotel Hotel { get; set; }
        public Categorie.Categorie Categorie { get; set; }

    }
}
