using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserOperationClaimDal : EfEntityRepositoryBase<UserOperationClaim, ApplicationDbContext>, IUserOperationClaimDal
    {
        public EfUserOperationClaimDal(ApplicationDbContext context):base(context)
        {
        }
    }
}
