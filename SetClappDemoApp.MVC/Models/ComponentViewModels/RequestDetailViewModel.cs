using Core.Resources.Enum;

namespace SetClappDemoApp.MVC.Models.ComponentViewModels
{
    public class RequestDetailViewModel
    {
        public int RequestId { get; set; }
        public string DocumentNumber { get; set; }
        public string? AdditionalDescription { get; set; }
        public string StartFrom { get; set; }
        public string FinishDate { get; set; }
        public string RequestType { get; set; }
        public int RequestStatus { get; set; }
        public int UserType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string Position { get; set; }
        public string ReplacerFirstName { get; set; }
        public string ReplacerLastName { get; set; }
        public string ReplacerFatherName { get; set; }
    }
}
