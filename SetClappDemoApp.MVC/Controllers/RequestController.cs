using Core.Entities.Dto;
using Core.Extensions;
using Core.Resources.Enum;
using Core.Utilities.Configuration.Helpers.Abstract;
using Core.Utilities.Security.Jwt;
using Microsoft.AspNetCore.Mvc;
using SelectPdf;
using Service.Business.Abstract;
using SetClappDemoApp.MVC.Models.ComponentViewModels;

namespace SetClappDemoApp.MVC.Controllers
{
    [Route("[controller]")]
    public class RequestController : Controller
    {
        private readonly IUserService _userService;
        private readonly IWorkerRequestService _workerRequestService;
        private readonly IConfigurationHelper _configHelper;
        private readonly IUserWorkerRequestService _userWorkerRequestService;
        public RequestController(IUserService userService,
                                 IWorkerRequestService workerRequestService,
                                 IUserWorkerRequestService userWorkerRequestService,
                                 IConfigurationHelper configurationHelper)
        {
            _userService= userService;
            _workerRequestService= workerRequestService;
            _userWorkerRequestService= userWorkerRequestService;
            _configHelper= configurationHelper;
        }


        [HttpPost("getrequestdetails")]
        public async Task<IActionResult> GetRequestDetails(int requestId)
        {
            var result = await GetRequestDetailViewModelAsync(requestId);

            return Ok(result);
        }

        [HttpGet("printrequest/{id}")]
        public async Task<IActionResult> PrintRequest(int id)
        {
            string html;
            byte[] pdf;

            var requestFormatModel = await GetRequestDetailViewModelAsync(id);

            using (var sr = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "documents", "pdfContentHtml.txt")))
            {
                var res = sr.ReadToEnd();
                html = res;
            }

            html = html.Replace(requestFormatModel);

            HtmlToPdf converter = new HtmlToPdf();
            PdfDocument document = converter.ConvertHtmlString(html);

            pdf = document.Save();
            document.Close();

            return File(pdf, "application/pdf");
        }


        [HttpPost("next")]
        public async Task<IActionResult> Next(int requestId)
        {
            var token = Request.Cookies["accessToken"];
            var userId = JwtHelper.GetUserId(token);
            var resultRequest = await _workerRequestService.GetAsync(x=>x.Id == requestId);
            var nextStatus = (int)resultRequest.Data.Status + 1;
            resultRequest.Data.Status = (RequestStatus)nextStatus;

            var emailDto = new SendgridEmailDto
            {
                ApiKey = _configHelper.GetDecryptedValue("EmailConfigSNDGR", "ApiKey"),
                DefaultEmailFrom = _configHelper.GetDecryptedValue("EmailConfigSNDGR", "DefaultEmailFrom"),
                TemplateId = _configHelper.GetDecryptedValue("EmailConfigSNDGR", "RequestReportTemplateId")
            };
            var updateResult = await _workerRequestService.UpdateAndAssignAsync(resultRequest.Data, userId, emailDto);

            if (!updateResult.Success)
                return BadRequest(updateResult.Responses);

            return Ok(updateResult.Success);
        }

        [HttpPost("reject")]
        public async Task<IActionResult> Reject(int requestId)
        {
            var resultRequest = await _workerRequestService.GetAsync(x => x.Id == requestId);
            if (resultRequest.Data.Status == RequestStatus.SentToManager)
                resultRequest.Data.Status = RequestStatus.ManagerDeclined;
            else
                resultRequest.Data.Status = RequestStatus.Declined;

            resultRequest.Data.AssignedWorkerId = 0;
            var updateResult = await _workerRequestService.UpdateAsync(resultRequest.Data);

            if (!updateResult.Success)
                return BadRequest(updateResult.Responses);

            return Ok(updateResult.Success);
        }

        private async Task<RequestDetailViewModel> GetRequestDetailViewModelAsync(int requestId)
        {
            var token = Request.Cookies["accessToken"];
            var userId = JwtHelper.GetUserId(token);
            var activeUser = await _userService.GetByIdAsync(userId);

            var userRequests = await _userWorkerRequestService.GetListAsync(x => x.RequestId == requestId);
            var userOwner = userRequests.Data.Where(x => x.IsRequestOwner).First().User;
            var replacer = userRequests.Data.Where(x => !x.IsRequestOwner).First().User;
            var workerRequest = userRequests.Data.Where(x => !x.IsRequestOwner).First().Request;

            RequestDetailViewModel result = new RequestDetailViewModel
            {
                FirstName = userOwner.FirstName,
                FatherName = userOwner.FatherName,
                LastName = userOwner.LastName,
                Position = userOwner.Position,
                ReplacerFirstName = replacer.FirstName,
                ReplacerFatherName = replacer.FatherName,
                ReplacerLastName = replacer.LastName,
                AdditionalDescription = workerRequest.AdditionalDescription,
                DocumentNumber = workerRequest.DocumentNumber,
                FinishDate = workerRequest.FinishDate.ToShortDateString(),
                StartFrom = workerRequest.StartFrom.ToShortDateString(),
                RequestType = workerRequest.RequestType.Translate(),
                RequestStatus = (int)workerRequest.Status,
                UserType = (int)activeUser.UserType,
                RequestId = requestId
            };

            return result;
        }
    }
}
