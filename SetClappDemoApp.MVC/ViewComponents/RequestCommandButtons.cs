using Core.Utilities.Security.Jwt;
using Microsoft.AspNetCore.Mvc;
using Service.Business.Abstract;

namespace SetClappDemoApp.MVC.ViewComponents
{
    public class RequestCommandButtons : ViewComponent
    {
        private readonly IUserService _userService;
        public RequestCommandButtons(IUserService userService)
        {
            _userService = userService;
        }

        public IViewComponentResult Invoke()
        {
            var token = Request.Cookies["accessToken"];
            var userId = JwtHelper.GetUserId(token);
            var user = _userService.GetById(userId);

            return View(user);
        }
    }
}
