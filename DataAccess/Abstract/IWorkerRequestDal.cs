using Core.DataAccess;
using Core.Entities.Concrete;
using System.Linq.Expressions;

namespace DataAccess.Abstract
{
    public interface IWorkerRequestDal : IEntityRepository<WorkerRequest>, IEntityQueryableRepository<WorkerRequest>
    {
        IList<WorkerRequest> GetWorkerRequestsWithRelations(Expression<Func<WorkerRequest, bool>> filter = null);
        Task<IList<WorkerRequest>> GetWorkerRequestsWithRelationsAsync(Expression<Func<WorkerRequest, bool>> filter = null);
    }
}
