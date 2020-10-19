using MediatR;

namespace Parmesan.Server.Commands.ApproveAuthorizationRequest.Domain
{
    public class ApproveAuthorizationRequestCommand : IRequest<string>
    {
        public int UserID { get; set; }
        public string TicketNumber { get; set; }
    }
}
