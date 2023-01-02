using Core.Resources.Enum;
using Core.Utilities.Security.Jwt;
using Microsoft.AspNetCore.Mvc;
using Service.Business.Abstract;
using SetClappDemoApp.MVC.Models.ComponentViewModels;

namespace SetClappDemoApp.MVC.ViewComponents
{
    public class CreateRequestButton : ViewComponent
    {
        private readonly IUserService _userService;
        public CreateRequestButton(IUserService userService)
        {
            _userService= userService;
        }
        public IViewComponentResult Invoke()
        {
            bool createOrNot;
            var token = Request.Cookies["accessToken"];
            var userId = JwtHelper.GetUserId(token);
            var user = _userService.GetById(userId);

            var viewModel = new CreateRequestViewModel
            {
                ActiveUser = user,
                ExistWorkers = _userService.GetList(x=>x.UserType == UserType.Worker && x.Id != userId && x.ChiefId == user.ChiefId).Data.ToList()
            };

            return View(viewModel);
        }
    }
}
