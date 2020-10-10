using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Parmesan.Application.Queries.VerifyAuthorizationRequest.Implementation
{
    using Domain;
    using Parmesan.Abstraction;
    using Parmesan.Domain;
    using Persistence.Abstraction;
    using ViewModels.Domain;

    internal class VerifyAuthorizationRequestHandler : IRequestHandler<VerifyAuthorizationRequestQuery, AuthorizationRequestViewModel>
    {
        private IParmesanDatabase Database { get; }
        private IScopeResolver ScopeResolver { get; }

        public VerifyAuthorizationRequestHandler(
            IParmesanDatabase database,
            IScopeResolver scopeResolver)
        {
            Database = database;
            ScopeResolver = scopeResolver;
        }

        public async Task<AuthorizationRequestViewModel> Handle(VerifyAuthorizationRequestQuery request, CancellationToken cancellationToken)
        {
            AuthorizationRequest authZ = request.AuthorizationRequest;
            Client client = await Database.Clients.FetchByPublicClientID(authZ.ClientID);

            if (authZ.ResponseType != ResponseType.code)
                throw new NotImplementedException();
            if (authZ.CodeChallengeMethod != CodeChallengeMethod.S256)
                throw new NotImplementedException();

            VerifyRedirects(authZ, client);

            List<string> resolvedScopes = authZ
                .Scope
                .Select(scope => ScopeResolver.Resolve(scope))
                .ToList();
            return new AuthorizationRequestViewModel
            {
                PublicClientID = client.PublicClientID,
                ClientDisplayName = client.DisplayName,
                RedirectUri = authZ.RedirectUri,
                ScopeDescriptions = resolvedScopes,
                State = authZ.State,
                CodeChallenge = authZ.CodeChallenge,
            };
        }
        private void VerifyRedirects(AuthorizationRequest request, Client client)
        {
            if (string.IsNullOrWhiteSpace(request.RedirectUri))
            {
                if (client.Redirects.Count > 1)
                    throw new InvalidOperationException("Must supply a redirect URI when more than one has been pre-registered");
                request.RedirectUri = client.Redirects[0].RedirectionUri;
            }
            else if (!client.Redirects.Any(redirect => request.RedirectUri == redirect.RedirectionUri))
                throw new InvalidOperationException("The requested redirection URI must match a pre-registered value");
        }
    }
}
