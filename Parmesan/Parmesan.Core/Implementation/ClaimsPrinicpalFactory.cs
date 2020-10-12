using System.Security.Claims;

namespace Parmesan.Implementation
{
    using Abstraction;
    using Domain;

    internal class ClaimsPrinicpalFactory : IClaimsPrincipalFactory
    {
        public ClaimsPrincipal CreateLoginForUser(string authenticationType, User user) =>
            new ClaimsPrincipal(BuildLoginUserIdentity(authenticationType, user));
        private ClaimsIdentity BuildLoginUserIdentity(string authenticationType, User user) =>
            new ClaimsIdentity(new[] { BuildLoginUserClaim(user.ID) }, authenticationType);
        private Claim BuildLoginUserClaim(int userID) =>
            new Claim(
                ClaimsPrincipalConstants.LoggedInUserIDClaimType,
                userID.ToString(),
                ClaimValueTypes.Integer32);
    }
}
