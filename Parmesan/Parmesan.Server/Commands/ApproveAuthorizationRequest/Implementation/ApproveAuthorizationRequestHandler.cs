using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;

namespace Parmesan.Server.Commands.ApproveAuthorizationRequest.Implementation
{
    using Domain;
    using Application.Domain;
    using Server.Abstraction;
    using Application.Commands.CreateAuthorizationCode.Domain;
    using Parmesan.Domain;

    internal class ApproveAuthorizationRequestHandler : IRequestHandler<ApproveAuthorizationRequestCommand, string>
    {
        private IAuthorizationTickets AuthorizationTickets { get; }
        private IMediator Mediator { get; }

        public ApproveAuthorizationRequestHandler(
            IAuthorizationTickets authorizationTickets, 
            IMediator mediator)
        {
            AuthorizationTickets = authorizationTickets;
            Mediator = mediator;
        }

        public async Task<string> Handle(ApproveAuthorizationRequestCommand request, CancellationToken cancellationToken)
        {
            VerifiedAuthorizationRequest authZ = AuthorizationTickets.Consume(request.TicketNumber);
            string authorizationCode = await Mediator.Send(new CreateAuthorizationCodeCommand
            {
                ClientID = authZ.Client.ID,
                UserID = request.UserID,
                Scope = ReassembleScopes(authZ.Scopes),
                CodeChallenge = authZ.CodeChallenge,
            });
            Dictionary<string, string> queryAdditions = new Dictionary<string, string>();
            queryAdditions.Add(AuthorizationRequest.StateName, authZ.State);
            queryAdditions.Add(ResponseType.code.ToString(), authorizationCode);

            return QueryHelpers.AddQueryString(authZ.RedirectUri, queryAdditions);
        }
        private string ReassembleScopes(List<string> scopes)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < scopes.Count; i++)
            {
                if (i > 0)
                    stringBuilder.Append(" ");
                stringBuilder.Append(scopes[i]);
            }
            return stringBuilder.ToString();
        }
    }
}
