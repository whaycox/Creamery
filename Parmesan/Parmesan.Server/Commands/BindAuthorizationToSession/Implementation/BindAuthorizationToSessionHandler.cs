using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Parmesan.Server.Commands.BindAuthorizationToSession.Implementation
{
    using Domain;
    using Parmesan.Domain;
    using CreateSession.Domain;

    internal class BindAuthorizationToSessionHandler : AsyncRequestHandler<BindAuthorizationToSessionCommand>
    {
        private IMediator Mediator { get; }

        public BindAuthorizationToSessionHandler(IMediator mediator)
        {
            Mediator = mediator;
        }

        protected async override Task Handle(BindAuthorizationToSessionCommand request, CancellationToken cancellationToken)
        {
            string sessionID = request.Session;
            if (string.IsNullOrWhiteSpace(sessionID))
                sessionID = await Mediator.Send(new CreateSessionCommand());

            throw new NotImplementedException();
        }
    }
}
