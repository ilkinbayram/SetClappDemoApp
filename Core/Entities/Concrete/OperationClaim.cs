using Core.Base.Entities.Concrete.Base;
using Core.Entities.Abstract;

namespace Core.Entities.Concrete
{
    public class OperationClaim : BaseEntity, IEntity
    {
        public OperationClaim()
        {
        }

        public string Name { get; set; }

        public virtual List<UserOperationClaim> UserOperationClaims { get; set; }
    }
}
