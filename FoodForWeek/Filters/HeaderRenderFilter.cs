using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FoodForWeek.Filters
{
    public class HeaderRenderFilter : IActionFilter
    {
        private const string  _loginAction="Login";
        private const string _registerAction="Register";
        private const string _controllerName="Account";
        public void OnActionExecuted(ActionExecutedContext context)
        {
            return;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            ClaimsPrincipal user = context.HttpContext.User;
            var currentController = (Controller)context.Controller;
            if (currentController.ControllerContext.ActionDescriptor.ControllerName ==_controllerName
                && (currentController.ControllerContext.ActionDescriptor.ActionName ==_loginAction
                || currentController.ControllerContext.ActionDescriptor.ActionName ==_registerAction))
                {
                    if(user.Identity.IsAuthenticated)
                    {
                        currentController.ViewData["SectionName"] = "Authenticated";
                        return;
                    }
                    else
                    {
                        currentController.ViewData["SectionName"] = "NotAuthenticated";
                        return;
                    }        
                }
            return;
        }
    }
}
