using Hotels.DataBase;
using Hotels.Models;
using Hotels.Models.Categorie;
using Hotels.Models.Hotel;
using Hotels.Models.Review;
using Hotels.net.Helpers.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hotels.Controllers
{
    public class HotelController:ControllerBase
    {

        private readonly DataBaseContext db;

        public HotelController(DataBaseContext db)
        {
            this.db = db;
        }
        [HttpPost("add_hotel")]
        [Authorization(Role.Admin)]
        public IActionResult AddHotel(Hotel_Repost hotel)
        {
            var hotelEntity = new Hotel
            {
                name = hotel.name,
                city = hotel.city
            };

            db.Hotel.Add(hotelEntity);
            db.SaveChanges();

            // Group hotels by city
            var hotelsByCity = db.Hotel
                .GroupBy(h => h.city)
                .ToList();

            return Ok(hotelsByCity);
        }
        [HttpGet("{id}")]
        [Authorization(Role.Admin,Role.User)]
        public IActionResult GetHotel(Guid id)
        {
            var hotel = db.Hotel.Find(id);
            Hotel_Request hotel1 = new Hotel_Request();
            hotel1.name = hotel.name;
            hotel1.city = hotel.city;
            hotel1.Id = hotel.Id;
            return Ok(hotel1);
        }
        [HttpDelete("{id}")]
        [Authorization(Role.Admin)]
        public IActionResult DeleteHotel(Guid id)
        {
            var hotel = db.Hotel.Find(id);
            db.Hotel.Remove(hotel);
            db.SaveChanges();
            return Ok();
        }
        [HttpGet("get_all_hotels")]
        [Authorization(Role.Admin,Role.User)]
        public IActionResult getAllHotels()
        {
            var hotels=db.Hotel.AsNoTracking().ToList();
            var hotels_request=new List<Hotel_Request>();
            foreach (var hotel in hotels)
            {
                var hotel_request=new Hotel_Request();
                hotel_request.name = hotel.name;
                hotel_request.city = hotel.city;
                hotel_request.Id = hotel.Id;
                hotels_request.Add(hotel_request);
            }
            return Ok(hotels_request);

        }
        [HttpGet("get_hotels_by_city")]
        [Authorization(Role.Admin,Role.User)]
        public IActionResult GetHotels_By_City(string city)
        {
            var hotels = db.Hotel.AsNoTracking().Where(x => x.city == city).ToList();
            var hotels_request= new List<Hotel_Request>();
            foreach (var hotel in hotels)
            {
                var hotel_request=new Hotel_Request();
                hotel_request.name = hotel.name;
                hotel_request.city = hotel.city;
                hotel_request.Id = hotel.Id;
                hotels_request.Add(hotel_request);
            }
            return Ok(hotels);
        }
        [HttpGet("get_hotel_with_categories_and_reviews/{id}")]
        [Authorization(Role.Admin, Role.User)]
        public IActionResult GetHotelWithCategoriesAndReviews(Guid id)
        {
            var hotel = db.Hotel
                .Include(h => h.hotel_categories)
                    .ThenInclude(hc => hc.Categorie)
                .Include(h => h.reviews)
                .FirstOrDefault(h => h.Id == id);

            if (hotel == null)
            {
                return NotFound();
            }

            // Map the hotel, categories, and reviews to the desired response model
            var hotelWithCategoriesAndReviews = new HotelWithCategoriesAndReviews
            {
                name = hotel.name,
                city = hotel.city,
                // Add other hotel properties as needed
                Categories = hotel.hotel_categories.Select(hc => new Categorie
                {
                    name = hc.Categorie.name
                    // Add other category properties as needed
                }).ToList(),
                Reviews = hotel.reviews.Select(r => new Review
                {
                    nota=r.nota,
                    descriere=r.descriere
                    
                }).ToList()
            };

            return Ok(hotelWithCategoriesAndReviews);
        }

    }
}
