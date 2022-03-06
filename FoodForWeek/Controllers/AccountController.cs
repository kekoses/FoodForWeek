using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Identity = Microsoft.AspNetCore.Identity;
using FoodForWeek.Library.Services.Interfaces;
using FoodForWeek.ViewModels;

namespace FoodForWeek.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly IMenuService _menuService;
        private readonly IUserService _userService;
        public AccountController(IMenuService menuService, IUserService userService)
        {
            _menuService = menuService;
            _userService = userService;
        }
        [HttpGet]
        [ResponseCache(NoStore = true)]
        [Route("~/")]
        [Route("~Account/Index")]
        public IActionResult Index(LoginViewModel loginVM=null)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(loginVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("~/Account/Login")]
        public async Task<IActionResult> Login([FromForm]LoginViewModel loginVM)
        {
            if (ModelState.IsValid)
            {
                Identity.SignInResult result=await _userService.CheckAuthForLoggingUserAsync(loginVM.LoggingUser);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index","Home");
                }
                if (result == Identity.SignInResult.Failed)
                {
                    return RedirectToAction("Register", "Account");
                }
            }
            return View("Index",loginVM);
        }
        [HttpGet]
        [Route("~/Account/Register")]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("~/Account/Register")]
        public async Task<IActionResult> Register([FromForm]RegisterViewModel registerVM)
        {
            if (ModelState.IsValid)
            {
                Identity.SignInResult result = await _userService.ProcessNewUserAsync(registerVM.RegisteringUser);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(registerVM);
        }
        [HttpGet]
        [Route("~/Account/log-out")]
        public async Task<IActionResult> Logout()
        {
            await _userService.Logout();
            return RedirectToAction("Index");
        }
    }
}
