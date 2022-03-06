using FoodForWeek.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FoodForWeek.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(HttpContext.User?.Identity?.IsAuthenticated ?? false);
        }
    }
}