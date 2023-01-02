using System.Security.Claims;

namespace Core.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static List<string> Claims(this ClaimsPrincipal claimsPrincipal, string claimType)
        {
            var result = claimsPrincipal?.FindAll(claimType)?.Select(x => x.Value).ToList();
            return result;
        }

        public static List<string> ClaimRoles(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal?.Claims(ClaimTypes.Role);
        }

        public static long GetUserId(this ClaimsPrincipal principal)
        {
            var claims = principal.Claims;
            var claimIdResult = claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            return claimIdResult == null ? default(long) : Convert.ToInt64(claimIdResult.Value);
        }

        public static string GetFullName(this ClaimsPrincipal principal)
        {
            var claims = principal.Claims;
            var claimFullNameResult = claims.SingleOrDefault(claim => claim.Type == ClaimTypes.Name);

            return claimFullNameResult?.Value;
        }

        public static string GetEmail(this ClaimsPrincipal principal)
        {
            var claims = principal.Claims;
            var claimEmailResult = claims.SingleOrDefault(claim => claim.Type == ClaimTypes.Email);

            return claimEmailResult?.Value;
        }
    }

}
