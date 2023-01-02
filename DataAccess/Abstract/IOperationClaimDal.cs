using Core.DataAccess;
using Core.Entities.Concrete;
using System.Linq.Expressions;

namespace DataAccess.Abstract
{
    public interface IOperationClaimDal : IEntityRepository<OperationClaim>, IEntityQueryableRepository<OperationClaim>
    {
    }
}
