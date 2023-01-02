using Operation.Abstract;

namespace Operation.Concrete.email.Sendgrid.Models.Template
{
    public class VerificationTemplate : IEmailTemplate
    {
        public string? Name { get; set; }
        public string? SecurityToken { get; set; }
    }
}
