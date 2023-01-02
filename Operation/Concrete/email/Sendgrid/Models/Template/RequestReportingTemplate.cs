using Operation.Abstract;

namespace Operation.Concrete.email.Sendgrid.Models.Template
{
    public class RequestReportingTemplate : IEmailTemplate
    {
        public string? DocumentNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FatherName { get; set; }
        public string? Position { get; set; }
        public string? StartFrom { get; set; }
        public string? FinishDate { get; set; }
        public string? RequestType { get; set; }


        public string? ReplacerFirstName { get; set; }
        public string? ReplacerLastName { get; set; }
        public string? ReplacerFatherName { get; set; }
        public string? AdditionalDescription { get; set; }
    }
}
