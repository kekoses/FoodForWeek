using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FoodForWeek.Filters
{
    public class HeaderRenderFilter : IAsyncActionFilter
    {
        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            ClaimsPrincipal user = context.HttpContext.User;
            var currentController = (Controller)context.Controller;
            if (user.Identity.IsAuthenticated)
            {
                currentController.ViewData["SectionName"] = "Authenticated";
                next.Invoke();
            }
            else
            {
                currentController.ViewData["SectionName"] = "NotAuthenticated";
                next.Invoke();
            }
            return Task.CompletedTask;
        }
    }
}
