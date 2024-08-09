using Microsoft.AspNetCore.Mvc;

namespace Mongo.API.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
