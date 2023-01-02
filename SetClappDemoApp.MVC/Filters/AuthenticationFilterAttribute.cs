using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SetClappDemoApp.MVC.Filters
{
    public class AuthenticationFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var accessToken = context.HttpContext.Request.Cookies["accessToken"];

            if (string.IsNullOrEmpty(accessToken))
                context.Result = new RedirectToRouteResult
                (
                new RouteValueDictionary(new
                {
                    action = "AccountOperate",
                    controller = "Auth"
                }));
        }
    }
}
