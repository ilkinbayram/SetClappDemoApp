using Core.Entities.Abstract;

namespace Core.Entities.Dto.User.Auth
{
    public class UserForLoginDto : IDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
