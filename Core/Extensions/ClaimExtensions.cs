using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Core.Extensions
{
    public static class ClaimExtensions
    {
        public static void AddEmail(this ICollection<Claim> claims, string email)
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Email,email));
        }

        public static void AddFullName(this ICollection<Claim> claims, string name, string surname)
        {
            claims.Add(new Claim(ClaimTypes.Name, $"{name} {surname}"));
        }

        public static void AddRoles(this ICollection<Claim> claims, string[] roles)
        {
            roles.ToList().ForEach(role=>claims.Add(new Claim(ClaimTypes.Role, role)));
        }

        public static void AddUserId(this ICollection<Claim> claims, int userId)
        {
            claims.Add(new Claim(ClaimTypes.NameIdentifier, userId.ToString()));
        }
    }
}
