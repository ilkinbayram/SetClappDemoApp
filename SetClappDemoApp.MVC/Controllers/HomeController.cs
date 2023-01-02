using Core.Entities.Dto.WorkerRequest;
using Core.Utilities.Security.Jwt;
using Microsoft.AspNetCore.Mvc;
using Service.Business.Abstract;
using SetClappDemoApp.MVC.Filters;
using SetClappDemoApp.MVC.Models;
using System.Diagnostics;

namespace SetClappDemoApp.MVC.Controllers
{
    [AuthenticationFilter]
    public class HomeController : Controller
    {
        private readonly IWorkerRequestService _workerRequestService;
        public HomeController(IWorkerRequestService workerRequestService)
        {
            _workerRequestService = workerRequestService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Index(CreateWorkerRequestDto workerRequestDto)
        {
            var token = Request.Cookies["accessToken"];

            var userId = JwtHelper.GetUserId(token);

            var requestAddResult = _workerRequestService.Add(workerRequestDto, userId);

            if (!requestAddResult.Success)
            {
                var errorViewModel = new ValidationErrorViewModel(requestAddResult.Success, requestAddResult.Responses.Select(x => x.Message).ToList());
                return Ok(errorViewModel);
            }

            var successResult = new SuccessViewModelWithRedirection { RedirectToUrl = Url.Action("index", "home") };

            return Ok(successResult);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}