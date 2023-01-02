using Core.DataAccess;
using Core.Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IUserWorkerRequestDal : IEntityRepository<UserWorkerRequest>, IEntityQueryableRepository<UserWorkerRequest>
    {
    }
}
