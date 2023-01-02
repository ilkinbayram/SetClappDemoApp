using AutoMapper;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Service.Business.Abstract;
using System.Linq.Expressions;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly IMapper _mapper;
        public UserManager(IUserDal userDal, IMapper mapper)
        {
            _userDal = userDal;
            _mapper = mapper;
        }

        #region SychronousOperations

        public IDataResult<IList<User>> GetList(Expression<Func<User, bool>> filter = null)
        {
            var response = _userDal.GetAll(filter);
            return new SuccessDataResult<IList<User>>(response);
        }
        public IDataResult<int> Add(User entity)
        {
            int affectedRows = _userDal.Add(entity);

            // TODO: Messages
            if (affectedRows <= 0)
                throw new Exception("Error");

            return new SuccessDataResult<int>(affectedRows, "Success");
        }
        public IDataResult<int> GetCount(Expression<Func<User, bool>> filter = null)
        {
            var response = _userDal.GetCount(filter);
            return new SuccessDataResult<int>(response);
        }
        public User? GetByMail(string email)
        {
            return _userDal.Get(u => u.Email == email);
        }
        public User? GetByUsername(string username)
        {
            return _userDal.Get(u => u.Username == username);
        }
        public User? ConfirmStatus(string securityToken)
        {
            var user = _userDal.Get(x => x.SecurityToken == securityToken);
            int affectedRows = 0;
            if (user != null)
            {
                user.SecurityToken = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10);
                user.ConfirmationStatus = true;
                affectedRows = _userDal.Update(user);
            }

            if (affectedRows > 0) return user;
            return null;
        }
        public User? GetById(int id)
        {
            return _userDal.GetById(id);
        }
        #endregion

        #region Asynchronous Operations
        public async Task<User?> ConfirmStatusAsync(string securityToken)
        {
            var user = await _userDal.GetAsync(x => x.SecurityToken == securityToken);
            int affectedRows = 0;
            if (user != null)
            {
                user.SecurityToken = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10);
                user.ConfirmationStatus = true;
                affectedRows = await _userDal.UpdateAsync(user);
            }

            if (affectedRows > 0) return user;
            return null;
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _userDal.GetByIdAsync(id);
        }
        public async Task<List<OperationClaim>> GetClaimsAsync(User user)
        {
            return await _userDal.GetClaimsAsync(user);
        }
        public async Task<IDataResult<IList<User>>> GetListAsync(Expression<Func<User, bool>> filter = null)
        {
            var response = await _userDal.GetAllAsync(filter);

            return new SuccessDataResult<IList<User>>(response);
        }

        #endregion
    }
}
