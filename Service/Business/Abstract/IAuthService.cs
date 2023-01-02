using Core.Entities.Concrete;
using Core.Entities.Dto.User.Auth;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;

namespace Service.Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserForRegisterDto userForRegisterDto);
        IDataResult<User> Login(UserForLoginDto userForLoginDto);
        IResult UserExists(string email, string username);

        Task<IDataResult<AccessToken>> CreateAccessTokenAsync(User user);
        Task<IDataResult<User>> ConfirmStatusAsync(string securityToken);
    }
}
