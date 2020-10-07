using MediatR;

namespace Parmesan.Application.Queries.VerifyAuthorizationRequest.Domain
{
    using Parmesan.Domain;
    using ViewModels.Domain;

    public class VerifyAuthorizationRequestQuery : IRequest<AuthorizationRequestViewModel>
    {
        public AuthorizationRequest AuthorizationRequest { get; set; }
    }
}
