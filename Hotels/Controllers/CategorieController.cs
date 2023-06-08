using Microsoft.AspNetCore.Mvc;

namespace Hotels.Controllers
{
    public class CategorieController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
