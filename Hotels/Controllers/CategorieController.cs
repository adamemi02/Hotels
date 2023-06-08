using Hotels.DataBase;
using Hotels.Models;
using Hotels.Models.Categorie;
using Hotels.net.Helpers.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hotels.Controllers
{
    public class CategorieController : Controller
    {
        private readonly DataBaseContext db;

        public CategorieController(DataBaseContext db)
        {
            this.db = db;
        }
        [HttpPost("add_categorie")]
        [Authorization(Role.Admin)]
        public IActionResult AddCategorie(Categorie_Repost categorie)
        {
            var categorie_exist = db.Categorie.Where(c => c.name == categorie.name).FirstOrDefault();
            if(categorie_exist != null)
            {
                return BadRequest("Categorie exist");
            }
            else
            {
                Categorie categorie1 = new Categorie();
                categorie1.name = categorie.name;
                db.Categorie.Add(categorie1);
                db.SaveChanges();
                
            }
           

            return Ok();
        }
        [HttpDelete("delete_categorie/{id}")]
        [Authorization(Role.Admin)]
        public IActionResult Delete_Categorie(Guid id)
        {
            var categorie = db.Categorie.Find(id);
            db.Categorie.Remove(categorie);
            db.SaveChanges();
            return Ok();
        }
        [HttpGet("get_categorie/{id}")]
        [Authorization(Role.Admin,Role.User)]
        public IActionResult Get_Categorie(Guid id)
        {
            var categorie = db.Categorie.Find(id);
            var categorie_request = new Categorie_Request();
            categorie_request.name = categorie.name;
            categorie_request.Id = categorie.Id;
            return Ok(categorie_request);
        }
        [HttpGet("get_all_categorie")]
        [Authorization(Role.Admin, Role.User)]
        public IActionResult Get_All_Categorie()
        {
            var categorie = db.Categorie.AsNoTracking().ToList();
            var categorie_request = new List<Categorie_Request>();
            foreach(var c in categorie)
            {
                var categorie1 = new Categorie_Request();
                categorie1.name = c.name;
                categorie1.Id = c.Id;
                categorie_request.Add(categorie1);
            }
            return Ok(categorie_request);
        }
    }
}
