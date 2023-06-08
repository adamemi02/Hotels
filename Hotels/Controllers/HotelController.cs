using Hotels.DataBase;
using Hotels.Models.Hotel;
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
        public IActionResult AddHotel(Hotel_Repost hotel)
        {
            var hotel1 = new Hotel();
            hotel1.name = hotel.name;
            hotel1.city = hotel.city;
            db.Hotel.Add(hotel1);
            db.SaveChanges();
            return Ok();
        }
        [HttpGet("{id}")]
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
        public IActionResult DeleteHotel(Guid id)
        {
            var hotel = db.Hotel.Find(id);
            db.Hotel.Remove(hotel);
            db.SaveChanges();
            return Ok();
        }
        [HttpGet("get_all_hotels")]
        public IActionResult getAllHotels()
        {
            var hotels=db.Hotel.AsNoTracking().ToList();
            return Ok(hotels);

        }
        [HttpGet("get_hotels_by_city")]
        public IActionResult GetHotels_By_City(string city)
        {
            var hotels = db.Hotel.AsNoTracking().Where(x => x.city == city).ToList();
            return Ok(hotels);
        }

    }
}
