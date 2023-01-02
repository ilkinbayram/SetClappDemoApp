using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfWorkerRequestDal : EfEntityRepositoryBase<WorkerRequest, ApplicationDbContext>, IWorkerRequestDal
    {
        public EfWorkerRequestDal(ApplicationDbContext applicationContext) : base(applicationContext)
        {
        }

        ~EfWorkerRequestDal()
        {
            this.Context.Dispose();
        }

        public IList<WorkerRequest> GetWorkerRequestsWithRelations(Expression<Func<WorkerRequest, bool>> filter = null)
        {
            var result = filter == null
                ? Context.Set<WorkerRequest>().Include(x => x.UserRequests).ThenInclude(x=>x.User).ToList()

                : Context.Set<WorkerRequest>().Where(filter).Include(x => x.UserRequests).ThenInclude(x => x.User).ToList();

            return result;
        }

        public async Task<IList<WorkerRequest>> GetWorkerRequestsWithRelationsAsync(Expression<Func<WorkerRequest, bool>> filter = null)
        {
            var result = filter == null
                ? Context.Set<WorkerRequest>().Include(x => x.UserRequests).ThenInclude(x => x.User).ToListAsync()
                : Context.Set<WorkerRequest>().Where(filter).Include(x => x.UserRequests).ThenInclude(x => x.User).ToListAsync();

            return await result;
        }
    }
}
