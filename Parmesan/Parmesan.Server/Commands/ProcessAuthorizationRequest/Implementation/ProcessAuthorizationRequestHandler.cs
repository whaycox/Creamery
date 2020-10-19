using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Parmesan.Server.Commands.ProcessAuthorizationRequest.Implementation
{
    using Application.Domain;
    using Application.Queries.VerifyAuthorizationRequest.Domain;
    using Domain;
    using Parmesan.Domain;
    using Server.Abstraction;
    using ViewModels.Domain;

    internal class ProcessAuthorizationRequestHandler : IRequestHandler<ProcessAuthorizationRequestCommand, AuthorizationRequestViewModel>
    {
        private IAuthorizationRequestParser RequsetParser { get; }
        private IMediator Mediator { get; }
        private IAuthorizationTickets AuthorizationTickets { get; }

        public ProcessAuthorizationRequestHandler(
            IAuthorizationRequestParser requsetParser,
            IMediator mediator,
            IAuthorizationTickets authorizationTickets)
        {
            RequsetParser = requsetParser;
            Mediator = mediator;
            AuthorizationTickets = authorizationTickets;
        }

        public async Task<AuthorizationRequestViewModel> Handle(ProcessAuthorizationRequestCommand request, CancellationToken cancellationToken)
        {
            AuthorizationRequest authZ = RequsetParser.Parse(request.Request);
            VerifiedAuthorizationRequest verifiedRequest = await Mediator.Send(new VerifyAuthorizationRequestQuery
            {
                AuthorizationRequest = authZ,
            });
            string ticketNumber = AuthorizationTickets.Create(verifiedRequest);
            return new AuthorizationRequestViewModel
            {
                TicketNumber = ticketNumber,
                ClientDisplayName = verifiedRequest.Client.DisplayName,
                ScopeDescriptions = verifiedRequest.ScopeDescriptions,
            };
        }
    }
}
