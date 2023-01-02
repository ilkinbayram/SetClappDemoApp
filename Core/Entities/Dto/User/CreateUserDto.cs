using Core.Entities.Abstract;
using Core.Entities.Concrete.Base;
using Core.Resources.Enum;

namespace Core.Entities.Dto.User
{
    public class CreateUserDto : BaseDto, ICreateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string Position { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public UserType UserType { get; set; }
        public int ChiefId { get; set; }
    }
}
