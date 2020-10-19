using System;

namespace Parmesan.Server.Implementation
{
    using Abstraction;
    using Controllers.Domain;
    using Parmesan.Domain;

    internal class AuthorizationRequestParser : IAuthorizationRequestParser
    {
        public AuthorizationRequest Parse(WebAuthorizationRequest webAuthorizationRequest)
        {
            AuthorizationRequest parsed = new AuthorizationRequest();

            if (!Enum.TryParse(webAuthorizationRequest.ResponseType, out ResponseType parsedResponseType))
                throw new ArgumentNullException(nameof(WebAuthorizationRequest.ResponseType));
            parsed.ResponseType = parsedResponseType;

            if (string.IsNullOrWhiteSpace(webAuthorizationRequest.ClientID))
                throw new ArgumentNullException(nameof(WebAuthorizationRequest.ClientID));
            parsed.ClientID = webAuthorizationRequest.ClientID;

            parsed.RedirectUri = webAuthorizationRequest.RedirectUri;
            parsed.State = webAuthorizationRequest.State;

            parsed.Scope.AddRange(
                webAuthorizationRequest.Scope?.Split(' ', StringSplitOptions.RemoveEmptyEntries));

            if (!Enum.TryParse(webAuthorizationRequest.CodeChallengeMethod, out CodeChallengeMethod parsedCodeChallengeMethod))
                throw new ArgumentNullException(nameof(WebAuthorizationRequest.CodeChallengeMethod));
            parsed.CodeChallengeMethod = parsedCodeChallengeMethod;

            if (string.IsNullOrWhiteSpace(webAuthorizationRequest.CodeChallenge))
                throw new ArgumentNullException(nameof(WebAuthorizationRequest.CodeChallenge));
            parsed.CodeChallenge = webAuthorizationRequest.CodeChallenge;

            return parsed;
        }
    }
}
