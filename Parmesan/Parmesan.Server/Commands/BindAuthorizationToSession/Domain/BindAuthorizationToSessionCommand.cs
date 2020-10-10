using MediatR;

namespace Parmesan.Server.Commands.BindAuthorizationToSession.Domain
{
    using Parmesan.Domain;

    public class BindAuthorizationToSessionCommand : IRequest
    {
        public string Session { get; set; }
        public AuthorizationRequest Request { get; set; }
    }
}
