using System.Security.Claims;

namespace Parmesan.Server
{
    using Parmesan.Domain;

    public static class ClaimsPrincipalExtensions
    {
        public static int LoggedInUserID(this ClaimsPrincipal principal)
        {
            Claim userIDClaim = principal.FindFirst(ClaimsPrincipalConstants.LoggedInUserIDClaimType);
            return int.Parse(userIDClaim.Value);
        }
    }
}
