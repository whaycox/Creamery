using System;
using System.Collections.Generic;
using System.Linq;

namespace Parmesan.Server.Implementation
{
    using Abstraction;
    using Domain;
    using Parmesan.Domain;

    internal class OAuthRequestFactory : IOAuthRequestFactory
    {
        private static Dictionary<string, ResponseType> ResponseTypeMap { get; } = ((IEnumerable<ResponseType>)Enum.GetValues(typeof(ResponseType))).ToDictionary(key => key.ToString());
        private static Dictionary<string, CodeChallengeMethod> CodeChallengeMethodMap { get; } = ((IEnumerable<CodeChallengeMethod>)Enum.GetValues(typeof(CodeChallengeMethod))).ToDictionary(key => key.ToString());

        public AuthorizationRequest Authorization(WebAuthorizationRequest request)
        {
            AuthorizationRequest created = new AuthorizationRequest();

            if (!ResponseTypeMap.TryGetValue(request.ResponseType, out ResponseType parsedResponseType))
                throw new ArgumentNullException(nameof(WebAuthorizationRequest.ResponseType));
            created.ResponseType = parsedResponseType;

            if (string.IsNullOrWhiteSpace(request.ClientID))
                throw new ArgumentNullException(nameof(WebAuthorizationRequest.ClientID));
            created.ClientID = request.ClientID;

            created.RedirectUri = request.RedirectUri;
            created.State = request.State;

            created
                .Scope
                .AddRange(
                    request
                        .Scope?
                        .Split(' ', StringSplitOptions.RemoveEmptyEntries));

            if (!CodeChallengeMethodMap.TryGetValue(request.CodeChallengeMethod, out CodeChallengeMethod parsedCodeChallengeMethod))
                throw new ArgumentNullException(nameof(WebAuthorizationRequest.CodeChallengeMethod));
            created.CodeChallengeMethod = parsedCodeChallengeMethod;

            if (string.IsNullOrWhiteSpace(request.CodeChallenge))
                throw new ArgumentNullException(nameof(WebAuthorizationRequest.CodeChallenge));
            created.CodeChallenge = request.CodeChallenge;

            return created;
        }

        public AccessTokenRequest AccessToken(WebAccessTokenRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
