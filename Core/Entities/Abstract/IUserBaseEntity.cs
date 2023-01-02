using Core.Resources.Enum;
using System.Reflection;

namespace Core.Entities.Abstract
{
    public interface IUserBaseEntity : IBaseEntity
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
        byte[] PasswordSalt { get; set; }
        byte[] PasswordHash { get; set; }
        string SecurityToken { get; set; }
    }
}
