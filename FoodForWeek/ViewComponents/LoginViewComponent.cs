using FoodForWeek.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FoodForWeek.ViewComponents
{
    public class LoginViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(LoginViewModel loginVM)
        {
            if(loginVM is null)
            {
                loginVM = new LoginViewModel();
                return View(loginVM);
            }
            return View(loginVM);
        }
    }
}
