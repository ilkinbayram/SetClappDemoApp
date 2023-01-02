using Core.Entities.Concrete;
using Core.Utilities.Results;
using System.Linq.Expressions;

namespace Service.Business.Abstract
{
    public interface IUserWorkerRequestService
    {
        IDataResult<IList<UserWorkerRequest>> GetList(Expression<Func<UserWorkerRequest, bool>> filter = null);
        Task<IDataResult<IList<UserWorkerRequest>>> GetListAsync(Expression<Func<UserWorkerRequest, bool>> filter = null);
    }
}
