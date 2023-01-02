using Core.Entities.Dto.User.Auth;
using Core.Resources.Enum;
using Core.Resources.OperationResources.Results;
using Core.Utilities.Configuration.Helpers.Abstract;
using Microsoft.AspNetCore.Mvc;
using Operation.Concrete.Base;
using Operation.Concrete.email.Sendgrid;
using Operation.Concrete.email.Sendgrid.Models.Request;
using Operation.Concrete.email.Sendgrid.Models.Template;
using Service.Business.Abstract;
using SetClappDemoApp.MVC.Models;

namespace SetClappDemoApp.MVC.Controllers
{
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly IConfigurationHelper _configHelper;
        public AuthController(IAuthService authService, IConfigurationHelper configHelper, IUserService userService)
        {
            _authService = authService;
            _configHelper = configHelper;
            _userService = userService;
        }

        [HttpGet("account")]
        public async Task<IActionResult> AccountOperate()
        {
            var chiefs = await _userService.GetListAsync(x => x.UserType == UserType.Manager);
            return View(chiefs.Data.ToList());
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(UserForRegisterDto registerDto)
        {
            var result = _authService.Register(registerDto);

            if (!result.Success)
            {
                var errorViewModel = new ValidationErrorViewModel(result.Success, result.Responses.Select(x => x.Message).ToList());
                return Ok(errorViewModel);
            }

            var verificationEmailSender = new Operation<SendVerificationEmailFunction<OperationResponse>, OperationResponse>();

            var verificationBody = new EmailBody<VerificationTemplate>
            {
                FromEmail = _configHelper.GetDecryptedValue("EmailConfigSNDGR", "DefaultEmailFrom"),
                ClientEmail = result.Data.Email,
                Template = new VerificationTemplate
                {
                    Name = $"{result.Data.FirstName} {result.Data.LastName}",
                    SecurityToken = result.Data.SecurityToken
                },

                TemplateId = _configHelper.GetDecryptedValue("EmailConfigSNDGR", "VerificationTemplateId"),
                ApiKey = _configHelper.GetDecryptedValue("EmailConfigSNDGR", "ApiKey")
            };

            // Here is the sending verification email place
            // var emailResult = await verificationEmailSender.ExecuteAsync(verificationBody);

            // When a user trys to register then the confirmation code should be sent by the mail and user should be redirected to the verification page
            // var successResult = new SuccessViewModelWithRedirection { RedirectToUrl = Url.Action("confirm", "auth") };

            var accessToken = await _authService.CreateAccessTokenAsync(result.Data);
            var cookieOpt = new CookieOptions();
            cookieOpt.Expires = accessToken.Data.Expiration;
            Response.Cookies.Append("accessToken", accessToken.Data.Token, cookieOpt);

            var successResult = new SuccessViewModelWithRedirection { RedirectToUrl = Url.Action("index", "home") };

            return Ok(successResult);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(UserForLoginDto loginDto)
        {
            var result = _authService.Login(loginDto);

            if (!result.Success)
            {
                var errorViewModel = new ValidationErrorViewModel(result.Success, result.Responses.Select(x => x.Message).ToList());
                return Ok(errorViewModel);
            }

            var accessToken = await _authService.CreateAccessTokenAsync(result.Data);
            var cookieOpt = new CookieOptions();
            cookieOpt.Expires = accessToken.Data.Expiration;
            Response.Cookies.Append("accessToken", accessToken.Data.Token, cookieOpt);
            var successResult = new SuccessViewModelWithRedirection { RedirectToUrl = Url.Action("index", "home") };
            return Ok(successResult);
        }

        [HttpGet("confirm")]
        public async Task<IActionResult> Confirm()
        {
            return View();
        }

        [HttpPost("confirm")]
        public async Task<IActionResult> Confirm(string securityToken)
        {
            var result = await _authService.ConfirmStatusAsync(securityToken);

            if (!result.Success) return BadRequest(result);

            var accessToken = await _authService.CreateAccessTokenAsync(result.Data);
            var cookieOpt = new CookieOptions();
            cookieOpt.Expires = accessToken.Data.Expiration;
            Response.Cookies.Append("accessToken", accessToken.Data.Token, cookieOpt);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Delete("accessToken");
            return RedirectToAction("Index", "Home");
        }
    }
}
