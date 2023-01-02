using Core.Entities.Abstract;
using Core.Resources.Enum;

namespace Core.Entities.Dto.User.Auth
{
    public class UserForRegisterDto : IDto
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string Position { get; set; }
        public int ChiefId { get; set; }
        public UserType UserType { get; set; }
    }
}
