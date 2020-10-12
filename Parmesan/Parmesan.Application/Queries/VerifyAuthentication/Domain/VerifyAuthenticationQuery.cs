using MediatR;
using System.Security.Claims;

namespace Parmesan.Application.Queries.VerifyAuthentication.Domain
{
    using Security.Abstraction;

    public class VerifyAuthenticationQuery : IRequest<ClaimsPrincipal>
    {
        public IAuthenticationData Authentication { get; set; }
    }
}
