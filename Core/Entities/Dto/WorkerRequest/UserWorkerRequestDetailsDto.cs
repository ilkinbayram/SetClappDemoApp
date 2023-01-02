using Core.Resources.Enum;

namespace Core.Entities.Dto.WorkerRequest
{
    public class UserWorkerRequestDetailsDto
    {
        public string DocumentNumber { get; set; }
        public string? AdditionalDescription { get; set; }
        public DateTime StartFrom { get; set; }
        public DateTime FinishDate { get; set; }
        public RequestType RequestType { get; set; }
        public UserType UserType { get; set; }
        public RequestStatus RequestStatus { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string Position { get; set; }
        public string ReplacerFirstName { get; set; }
        public string ReplacerLastName { get; set; }
        public string ReplacerFatherName { get; set; }
    }
}
