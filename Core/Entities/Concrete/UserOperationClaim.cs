using Core.Base.Entities.Concrete.Base;
using Core.Entities.Abstract;

namespace Core.Entities.Concrete
{
    public class UserOperationClaim: BaseEntity, IEntity
    {
        public UserOperationClaim()
        {
        }

        public int UserId { get; set; }
        public int OperationClaimId { get; set; }
        public virtual User User { get; set; }
        public virtual OperationClaim OperationClaim { get; set; }
    }
}
