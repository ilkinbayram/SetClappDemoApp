using Core.DataAccess;
using Core.Entities.Concrete;
using System.Linq.Expressions;

namespace DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>, IEntityQueryableRepository<User>
    {
        List<OperationClaim> GetClaims(User user);
        Task<List<OperationClaim>> GetClaimsAsync(User user);
    }
}
