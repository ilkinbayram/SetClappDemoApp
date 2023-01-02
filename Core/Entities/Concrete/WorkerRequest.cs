using Core.Base.Entities.Concrete.Base;
using Core.Resources.Enum;

namespace Core.Entities.Concrete
{
    public class WorkerRequest : BaseEntity
    {
        public WorkerRequest()
        {
            if (UserRequests == null)
                UserRequests = new List<UserWorkerRequest>();
        }

        public string DocumentNumber { get; set; }
        public string? AdditionalDescription { get; set; }
        public DateTime StartFrom { get; set; }
        public DateTime FinishDate { get; set; }
        public RequestType RequestType { get; set; }
        public RequestStatus Status { get; set; }

        public int AssignedWorkerId { get; set; }

        public virtual List<UserWorkerRequest> UserRequests { get; set; }
    }
}
