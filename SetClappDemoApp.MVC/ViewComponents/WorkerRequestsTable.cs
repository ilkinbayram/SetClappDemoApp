using Core.Utilities.Security.Jwt;
using Microsoft.AspNetCore.Mvc;
using Service.Business.Abstract;
using SetClappDemoApp.MVC.Models.ComponentViewModels;

namespace SetClappDemoApp.MVC.ViewComponents
{
    public class WorkerRequestsTable : ViewComponent
    {
        private readonly IUserService _userService;
        private readonly IWorkerRequestService _workerRequestService;
        private readonly IUserWorkerRequestService _userWorkerRequestService;
        public WorkerRequestsTable(IUserService userService, 
                                   IWorkerRequestService workerRequestService,
                                   IUserWorkerRequestService userWorkerRequestService)
        {
            _userService = userService;
            _workerRequestService = workerRequestService;
            _userWorkerRequestService = userWorkerRequestService;
        }

        public IViewComponentResult Invoke()
        {
            List<UserWorkerRequestViewModel> result = new List<UserWorkerRequestViewModel>();
            var token = Request.Cookies["accessToken"];
            var userId = JwtHelper.GetUserId(token);

            var user = _userService.GetById(userId);

            var createdRequestsIds = _userWorkerRequestService.GetList(x => x.IsRequestOwner && x.UserId == userId).Data.Select(x=>x.RequestId).ToList();
            var workerRequests = _workerRequestService.GetListWithRelations(x => x.AssignedWorkerId == userId || createdRequestsIds.Contains(x.Id)).Data;

            foreach (var request in workerRequests)
            {
                var ownerUser = request.UserRequests.FirstOrDefault(x => x.IsRequestOwner).User;
                result.Add(new UserWorkerRequestViewModel
                {
                    CreatedDate = request.Created_at.Value.Date,
                    DocumentNumber= request.DocumentNumber,
                    FullName = $"{ownerUser.FirstName} {ownerUser.LastName}",
                    Position = ownerUser.Position,
                    Status = request.Status,
                    WorkerRequestId = request.Id
                });
            }

            return View(result);
        }
    }
}
