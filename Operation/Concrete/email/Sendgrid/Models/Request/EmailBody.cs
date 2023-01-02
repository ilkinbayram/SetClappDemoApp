using Operation.Abstract;
using Operation.Concrete.email.Sendgrid.Models.Base;

namespace Operation.Concrete.email.Sendgrid.Models.Request
{
    public class EmailBody<TypeTemplateModel> : MailConfiguration
        where TypeTemplateModel : class, IEmailTemplate, new()
    {
        public EmailBody()
        {
        }

        public EmailBody(string templateId, string apiKey) : base(templateId, apiKey)
        {
        }

        public string? ClientEmail { get; set; }
        public string? FromEmail { get; set; }
        public TypeTemplateModel? Template { get; set; }
    }
}
