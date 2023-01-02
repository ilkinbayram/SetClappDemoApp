using Core.Resources.OperationResources.Results;
using Operation.Abstract;
using Operation.Concrete.Base;
using Operation.Concrete.email.Sendgrid.Models.Request;
using Operation.Concrete.email.Sendgrid.Models.Template;
using SendGrid.Helpers.Mail;
using SendGrid;

namespace Operation.Concrete.email.Sendgrid
{
    public class SendReportEmailFunction<TResult> : Function<TResult>
        where TResult : class, IOperationResponse, new()
    {
        public override async Task<TResult> DoExecuteAsync(IOperationModel body)
        {
            TResult result = new TResult();
            EmailBody<RequestReportingTemplate> model = (EmailBody<RequestReportingTemplate>)body;

            var client = new SendGridClient($"{model.ApiKey}");

            var sendgridReportingMessage = new SendGridMessage();

            sendgridReportingMessage.SetFrom(model.FromEmail, "Sender : SetClapp Server");
            sendgridReportingMessage.AddTo(model.ClientEmail, "Request Reporting");
            sendgridReportingMessage.SetTemplateId($"{model.TemplateId}");
            sendgridReportingMessage.SetTemplateData(model.Template);

            var responseOperation = await client.SendEmailAsync(sendgridReportingMessage);
            result.IsSuccess = responseOperation.IsSuccessStatusCode;

            return result;
        }
    }
}
