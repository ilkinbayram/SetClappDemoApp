using Core.Entities.Abstract;

namespace Core.Entities.Concrete
{
    public class UserWorkerRequest : IEntity
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }

        public bool IsRequestOwner { get; set; }

        public int UserId { get; set; }
        public int RequestId { get; set; }

        public virtual User User { get; set; }
        public virtual WorkerRequest Request { get; set; }
    }
}
