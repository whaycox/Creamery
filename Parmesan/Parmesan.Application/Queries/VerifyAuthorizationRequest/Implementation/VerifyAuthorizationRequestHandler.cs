using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Parmesan.Application.Queries.VerifyAuthorizationRequest.Implementation
{
    using Domain;
    using ViewModels.Domain;
    using Persistence.Abstraction;
    using Parmesan.Domain;

    internal class VerifyAuthorizationRequestHandler : IRequestHandler<VerifyAuthorizationRequestQuery, AuthorizationRequestViewModel>
    {
        private IParmesanDatabase Database { get; }

        public VerifyAuthorizationRequestHandler(
            IParmesanDatabase database)
        {
            Database = database;
        }

        public async Task<AuthorizationRequestViewModel> Handle(VerifyAuthorizationRequestQuery request, CancellationToken cancellationToken)
        {
            AuthorizationRequest authZ = request.AuthorizationRequest;
            Client client = await Database.Clients.FetchByPublicClientID(authZ.ClientID);


            throw new NotImplementedException();
        }
    }
}
