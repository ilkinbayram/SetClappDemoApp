using AutoMapper;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Service.Business.Abstract;
using System.Linq.Expressions;

namespace Service.Business.Concrete
{
    public class UserWorkerRequestManager : IUserWorkerRequestService
    {
        private readonly IMapper _mapper;
        private readonly IUserWorkerRequestDal _userWorkerRequestDal;
        public UserWorkerRequestManager(IUserWorkerRequestDal userWorkerRequestDal,
                                        IMapper mapper)
        {
            _mapper = mapper;
            _userWorkerRequestDal = userWorkerRequestDal;
        }


        public IDataResult<IList<UserWorkerRequest>> GetList(Expression<Func<UserWorkerRequest, bool>> filter = null)
        {
            var response = _userWorkerRequestDal.GetAll(filter);
            return new SuccessDataResult<IList<UserWorkerRequest>>(response);
        }

        public async Task<IDataResult<IList<UserWorkerRequest>>> GetListAsync(Expression<Func<UserWorkerRequest, bool>> filter = null)
        {
            var response = await _userWorkerRequestDal.GetAllAsync(filter);

            return new SuccessDataResult<IList<UserWorkerRequest>>(response);
        }
    }
}
