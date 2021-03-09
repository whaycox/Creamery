using MediatR;

namespace Parmesan.Application.Commands.CreateAuthorizationTicket.Domain
{
    using Parmesan.Domain;

    public class CreateAuthorizationTicketCommand : IRequest<AuthorizationTicket>
    {
        public AuthorizationRequest Request { get; set; }
    }
}
