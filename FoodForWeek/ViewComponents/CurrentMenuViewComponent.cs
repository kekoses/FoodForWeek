using FoodForWeek.Library.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodForWeek.ViewComponents
{
    public class CurrentMenuViewComponent : ViewComponent
    {
        private readonly IMenuService _menuService;

        public CurrentMenuViewComponent(IMenuService menuService)
        {
            _menuService = menuService;
        }


        public IViewComponentResult Invoke()
        {
            var dishes = _menuService.GetCurrentMenu();
            return View(dishes);
        }
    }
}
