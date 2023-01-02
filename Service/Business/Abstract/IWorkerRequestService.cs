using Core.Entities.Concrete;
using Core.Entities.Dto;
using Core.Entities.Dto.WorkerRequest;
using Core.Utilities.Results;
using System.Linq.Expressions;

namespace Service.Business.Abstract
{
    public interface IWorkerRequestService
    {
        Task<IDataResult<int>> UpdateAndAssignAsync(WorkerRequest entity, int userId, SendgridEmailDto emailDto);
        Task<IDataResult<WorkerRequest>> GetAsync(Expression<Func<WorkerRequest, bool>> filter);
        Task<IDataResult<int>> UpdateAsync(WorkerRequest entity);

        IDataResult<IList<WorkerRequest>> GetListWithRelations(Expression<Func<WorkerRequest, bool>> filter = null);
        IDataResult<int> Add(CreateWorkerRequestDto entity, int activeUserId);
    }
}
