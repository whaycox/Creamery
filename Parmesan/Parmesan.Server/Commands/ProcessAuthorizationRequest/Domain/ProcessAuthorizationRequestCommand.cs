using MediatR;

namespace Parmesan.Server.Commands.ProcessAuthorizationRequest.Domain
{
    using Controllers.Domain;
    using ViewModels.Domain;

    public class ProcessAuthorizationRequestCommand : IRequest<AuthorizationRequestViewModel>
    {
        public WebAuthorizationRequest Request { get; set; }
    }
}
