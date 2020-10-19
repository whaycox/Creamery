using Microsoft.AspNetCore.Mvc;

namespace Parmesan.Server.Controllers.Domain
{
    using Parmesan.Domain;

    public class WebAuthorizationRequest
    {
        [BindProperty(Name = AuthorizationRequest.ResponseTypeName)]
        public string ResponseType { get; set; }

        [BindProperty(Name = AuthorizationRequest.ClientIDName)]
        public string ClientID { get; set; }

        [BindProperty(Name = AuthorizationRequest.RedirectUriName)]
        public string RedirectUri { get; set; }

        [BindProperty(Name = AuthorizationRequest.ScopeName)]
        public string Scope { get; set; }

        [BindProperty(Name = AuthorizationRequest.StateName)]
        public string State { get; set; }

        [BindProperty(Name = AuthorizationRequest.CodeChallengeName)]
        public string CodeChallenge { get; set; }

        [BindProperty(Name = AuthorizationRequest.CodeChallengeMethodName)]
        public string CodeChallengeMethod { get; set; }
    }
}
