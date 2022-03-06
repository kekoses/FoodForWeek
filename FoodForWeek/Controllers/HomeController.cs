using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodForWeek.Controllers
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        [Authorize]
        [HttpGet]
        [Route("~/Index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
