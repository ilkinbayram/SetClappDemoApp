using Core.Base.Entities.Concrete.Base;
using Core.Entities.Abstract;
using Core.Resources.Enum;

namespace Core.Entities.Concrete
{
    public class User : BaseEntity, IEntity, IUserBaseEntity
    {
        public User()
        {
            if (UserRequests == null)
                UserRequests = new List<UserWorkerRequest>();
            if (UserOperationClaims == null)
                UserOperationClaims = new List<UserOperationClaim>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string Position { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public UserType UserType { get; set; }
        public int ChiefId { get; set; }
        public string SecurityToken { get; set; }
        public int SecurityCount { get; set; }
        public bool ConfirmationStatus { get; set; }

        public virtual List<UserOperationClaim> UserOperationClaims { get; set; }
        public virtual List<UserWorkerRequest> UserRequests { get; set; }
    }
}
