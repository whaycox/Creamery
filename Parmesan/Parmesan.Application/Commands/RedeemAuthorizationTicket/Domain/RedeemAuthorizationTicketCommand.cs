using MediatR;

namespace Parmesan.Application.Commands.RedeemAuthorizationTicket.Domain
{
    public class RedeemAuthorizationTicketCommand : IRequest<string>
    {
        public int UserID { get; set; }
        public string TicketNumber { get; set; }
    }
}
