using Bogus;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Entities.Dto.User.Auth;
using Core.Resources.Enum;
using Core.Utilities.Generators;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using Service.Business.Abstract;
using Service.ValidationRules.FluentValidation;

namespace Service.Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        public IResult UserExists(string email, string username)
        {
            if (_userService.GetByMail(email) != null ||
                _userService.GetByUsername(username) != null)
            {
                // TODO : Message
                return new ErrorResult("Fail");
            }
            return new SuccessResult();
        }


        [ValidationAspect(typeof(UserRegisterValidator), typeof(User), Priority = 1)]
        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto)
        {
            if (!UserExists(userForRegisterDto.Email, userForRegisterDto.Username).Success)
                return new ErrorDataResult<User>(null, "Email or Username is already exist!");

            var random = new Random();
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userForRegisterDto.Password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Email = userForRegisterDto.Email,
                Username = userForRegisterDto.Username,
                FirstName = userForRegisterDto.FirstName,
                FatherName = userForRegisterDto.FatherName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                // First registration time the Confirm status should be false for the confirmation
                // ConfirmationStatus = false,
                ConfirmationStatus = true,
                IsActive = true,
                Position = userForRegisterDto.Position,
                UserType = userForRegisterDto.UserType,
                SecurityToken = StringGenerator.GenerateSecurityToken(),
                SecurityCount = random.Next(100000, 999999),
                Created_at = DateTime.UtcNow,
                Created_by = userForRegisterDto.Username,
                Modified_at = DateTime.UtcNow,
                Modified_by = userForRegisterDto.Username,
                ChiefId= userForRegisterDto.ChiefId
            };

            var result = _userService.Add(user);

            if (user.UserType == UserType.Manager)
            {
                int chiefId = user.Id;

                for (int i = 0; i < 2; i++)
                {
                    int count = _userService.GetCount().Data + i + 1;

                    var fatherFaker = new Faker<User>()
                    .RuleFor(x => x.FirstName, p => p.Person.FirstName);

                    var faker = new Faker<User>()
                    .RuleFor(x => x.FirstName, p => p.Person.FirstName)
                    .RuleFor(x => x.LastName, p => p.Person.LastName)
                    .RuleFor(x => x.FatherName, p => p.Person.FirstName)
                    .RuleFor(x => x.Position, p => p.Person.Company.Name)
                    .RuleFor(x => x.Email, p => p.Person.Email); 

                    var fakeData = faker.Generate();
                    var fatherFakeData = fatherFaker.Generate();

                    var worker = new User
                    {
                        FirstName = fakeData.FirstName,
                        LastName = fakeData.LastName,
                        FatherName = fatherFakeData.FirstName,
                        UserType = UserType.Worker,
                        Username = "userworker"+count.ToString(),
                        Position = fakeData.Position,
                        Email = fakeData.Email,
                        ChiefId = chiefId,
                        Created_at = DateTime.UtcNow,
                        Modified_at = DateTime.UtcNow,
                        Created_by = "System Seed",
                        Modified_by = "System Seed",
                        IsActive = true,
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt,
                        SecurityCount = 111111,
                        ConfirmationStatus = true,
                        SecurityToken = StringGenerator.GenerateSecurityToken()
                    };

                    int resultFakerCount = _userService.Add(worker).Data;
                }
            }
            // TODO : Message
            return new SuccessDataResult<User>(user, "Success");
        }

        [ValidationAspect(typeof(UserForLoginValidator), typeof(User), Priority = 1)]
        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByUsername(userForLoginDto.Username);
            if (userToCheck == null)
            {
                // TODO : Message
                return new ErrorDataResult<User>("Fail");
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                // TODO : Message
                return new ErrorDataResult<User>("Fail");
            }
            // TODO : Message
            return new SuccessDataResult<User>(userToCheck, "Fail");
        }

        public async Task<IDataResult<AccessToken>> CreateAccessTokenAsync(User user)
        {
            var claims = await _userService.GetClaimsAsync(user);
            var accessToken = _tokenHelper.CreateToken(user, claims, false);
            // TODO : Message
            return new SuccessDataResult<AccessToken>(accessToken, "Success");
        }

        public async Task<IDataResult<User>> ConfirmStatusAsync(string securityToken)
        {
            var userResult = await _userService.ConfirmStatusAsync(securityToken);
            if (userResult == null) return new ErrorDataResult<User>(null, "User was not found!");
            return new SuccessDataResult<User>(userResult);
        }
    }
}
