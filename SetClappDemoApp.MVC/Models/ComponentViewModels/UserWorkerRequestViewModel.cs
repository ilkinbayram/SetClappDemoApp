using Core.Resources.Enum;

namespace SetClappDemoApp.MVC.Models.ComponentViewModels
{
    public class UserWorkerRequestViewModel
    {
        public int WorkerRequestId { get; set; }
        public string DocumentNumber { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public DateTime CreatedDate { get; set; }
        public RequestStatus Status { get; set; }
    }
}
