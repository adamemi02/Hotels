using Hotels.DataBase;
using Hotels.Models;
using Hotels.Models.Hotel;
using Hotels.Models.Review;
using Hotels.net.Helpers.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hotels.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly DataBaseContext db;

        public ReviewController(DataBaseContext db)
        {
            this.db = db;
        }
        [HttpPost("review")]
        [Authorization(Role.Admin, Role.User)]
        public IActionResult AddReview(Review_Repost review)
        {
            var review1 = new Review();
            review1.nota = review.nota;
            review1.descriere = review.descriere;
            review1.HotelId = review.idHotel;
            db.Review.Add(review1);
            db.SaveChanges();
            return Ok(review1);


            
        }

        [HttpDelete("delete{id}")]
        [Authorization(Role.Admin)]
        public IActionResult DeleteReview(Guid id)
        {
            var review = db.Review.Where(x => x.Id == id).FirstOrDefault();
            db.Review.Remove(review);
            db.SaveChanges();
            return NoContent();
        }

        [HttpGet("get_reviews")]
        [Authorization(Role.Admin, Role.User)]
        public IActionResult Get_Reviews()
        {
            var review = db.Review.ToList();
            var reviews = new List<Review_Repost>();
            foreach (var i in review)
            {
                var review2 = new Review_Repost();
                review2.nota = i.nota;
                review2.descriere = i.descriere;
                review2.idHotel = i.HotelId;
                reviews.Add(review2);
            }
            return Ok(reviews);
        }
        [HttpPost("get_reviews_by_hotel{IdHotel}")]
        [Authorization(Role.Admin, Role.User)]
        public IActionResult Get_Reviews_by_Hotel(Guid IdHotel)
        {
            var review = db.Review.Where(x => x.HotelId == IdHotel).ToList();
            var reviews = new List<Review_Repost>();
            foreach (var i in review)
            {
                var review2 = new Review_Repost();
                review2.nota = i.nota;
                review2.descriere = i.descriere;
                review2.idHotel = i.HotelId;
                reviews.Add(review2);
            }
            return Ok(reviews);
        }
        

    }
}
