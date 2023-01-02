using Core.Resources.OperationResources.Results;
using Operation.Abstract;
using Operation.Concrete.Base;
using Operation.Concrete.email.Sendgrid.Models.Request;
using Operation.Concrete.email.Sendgrid.Models.Template;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Operation.Concrete.email.Sendgrid
{
    public class SendVerificationEmailFunction<TResult> : Function<TResult>
        where TResult : class, IOperationResponse, new()
    {
        public override async Task<TResult> DoExecuteAsync(IOperationModel body)
        {
            TResult result = new TResult();
            EmailBody<VerificationTemplate> model = (EmailBody<VerificationTemplate>)body;

            var client = new SendGridClient($"{model.ApiKey}");

            var sendgridVerificationMessage = new SendGridMessage();

            sendgridVerificationMessage.SetFrom(model.FromEmail, "Sender : SetClapp Server");
            sendgridVerificationMessage.AddTo(model.ClientEmail, "Server Confirmation");
            sendgridVerificationMessage.SetTemplateId($"{model.TemplateId}");
            sendgridVerificationMessage.SetTemplateData(model.Template);

            var responseOperation = await client.SendEmailAsync(sendgridVerificationMessage);
            result.IsSuccess = responseOperation.IsSuccessStatusCode;

            return result;
        }
    }
}
