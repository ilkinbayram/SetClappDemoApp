using Core.Entities.Concrete;
using Core.Utilities.Results;
using System.Linq.Expressions;

namespace Service.Business.Abstract
{
    public interface IUserService
    {
        User? GetByMail(string email);
        User? GetById(int id);
        User? ConfirmStatus(string securityToken);
        User? GetByUsername(string username);
        IDataResult<int> Add(User entity);
        IDataResult<IList<User>> GetList(Expression<Func<User, bool>> filter = null);
        IDataResult<int> GetCount(Expression<Func<User, bool>> filter = null);
        Task<List<OperationClaim>> GetClaimsAsync(User user);
        Task<User?> GetByIdAsync(int id);
        Task<User?> ConfirmStatusAsync(string securityToken);
        Task<IDataResult<IList<User>>> GetListAsync(Expression<Func<User, bool>> filter = null);
    }
}
