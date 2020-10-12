using System.Security.Claims;

namespace Parmesan.Abstraction
{
    using Domain;

    public interface IClaimsPrincipalFactory
    {
        ClaimsPrincipal CreateLoginForUser(string authenticationType, User user);
    }
}
