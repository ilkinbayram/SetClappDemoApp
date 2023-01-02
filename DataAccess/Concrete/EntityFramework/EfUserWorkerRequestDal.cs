using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserWorkerRequestDal : EfEntityRepositoryBase<UserWorkerRequest, ApplicationDbContext>, IUserWorkerRequestDal
    {
        public EfUserWorkerRequestDal(ApplicationDbContext applicationContext) : base(applicationContext)
        {
        }

        ~EfUserWorkerRequestDal()
        {
            this.Context.Dispose();
        }
    }
}
