using Operation.Abstract;

namespace Operation.Concrete.email.Sendgrid.Models.Base
{
    public class MailConfiguration : IOperationModel
    {
        public MailConfiguration()
        {
        }

        public MailConfiguration(string templateId, string apiKey)
        {
            TemplateId = templateId;
            ApiKey = apiKey;
        }

        public virtual string? TemplateId { get; set; }
        public virtual string? ApiKey { get; set; }
    }
}
