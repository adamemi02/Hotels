using Hotels.DataBase;
using Hotels.Models.Hotel_Categorie;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hotels.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Hotel_CategorieController : ControllerBase
    {
        private readonly DataBaseContext db;

        public Hotel_CategorieController(DataBaseContext db)
        {
            this.db = db;
        }

        [HttpPost("add_hotel_la_o_categorie")]
        public IActionResult Add(Hotel_CategorieNeed hotel)
        {
            var hotel_categorie = new Hotel_Categorie();

            hotel_categorie.HotelId = hotel.HotelId;
               hotel_categorie.CategorieId = hotel.CategorieId;
            


            db.Hotel_Categorie.Add(hotel_categorie);
            db.SaveChanges();
            var hotelsByCategory = db.Hotel_Categorie
                .GroupBy(h => h.HotelId)
                .ToList();
            var hotelsByCategory_Need=new List<Hotel_CategorieNeed>();

            foreach (var hotelByCategory in hotelsByCategory)
            {
                var hotelByCategory_Need = new Hotel_CategorieNeed();
                hotelByCategory_Need.HotelId = hotelByCategory.Key;
                hotelByCategory_Need.CategorieId = hotelByCategory.FirstOrDefault().CategorieId;
                hotelsByCategory_Need.Add(hotelByCategory_Need);
            }


            return Ok(hotelsByCategory_Need);
        }

        [HttpDelete("{IdCategory}")]
        public IActionResult Delete(Guid IdCategory)
        {
            var hotels_categorie = db.Hotel_Categorie.Where(h => h.CategorieId == IdCategory).ToList();
            foreach (var hotel_categorie in hotels_categorie)
            {
                db.Hotel_Categorie.Remove(hotel_categorie);
            }
            db.SaveChanges();
            return Ok();
        }
        [HttpGet("{IdCategory}")]
        public IActionResult get_hotels_by_category(Guid IdCategory)
        {
            var hotels_categorie = db.Hotel_Categorie.Where(h => h.CategorieId == IdCategory).ToList();
            var hotels_categorie_need=new List<Hotel_CategorieNeed>();
            foreach(var hotel_categorie in hotels_categorie)
            {
                var hotel_categorie_need = new Hotel_CategorieNeed();
                hotel_categorie_need.HotelId = hotel_categorie.HotelId;
                hotel_categorie_need.CategorieId = hotel_categorie.CategorieId;
                hotels_categorie_need.Add(hotel_categorie_need);
            }
            return Ok(hotels_categorie_need);
        }
        [HttpGet("get_all")]
        public IActionResult get_all()
        {
            var hotels_categorie = db.Hotel_Categorie.ToList();
            var hotels_categorie_need = new List<Hotel_CategorieNeed>();
            foreach (var hotel_categorie in hotels_categorie)
            {
                var hotel_categorie_need = new Hotel_CategorieNeed();
                hotel_categorie_need.HotelId = hotel_categorie.HotelId;
                hotel_categorie_need.CategorieId = hotel_categorie.CategorieId;
                hotels_categorie_need.Add(hotel_categorie_need);
            }
            return Ok(hotels_categorie_need);
        }

    }
}
