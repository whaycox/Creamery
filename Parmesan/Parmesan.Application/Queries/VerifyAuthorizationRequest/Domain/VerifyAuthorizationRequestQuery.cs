using MediatR;

namespace Parmesan.Application.Queries.VerifyAuthorizationRequest.Domain
{
    using Parmesan.Domain;
    using Application.Domain;

    public class VerifyAuthorizationRequestQuery : IRequest<VerifiedAuthorizationRequest>
    {
        public AuthorizationRequest AuthorizationRequest { get; set; }
    }
}
