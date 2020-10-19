using System.Collections.Generic;

namespace Parmesan.Application.Domain
{
    using Parmesan.Domain;

    public class VerifiedAuthorizationRequest
    {
        public Client Client { get; set; }
        public string RedirectUri { get; set; }
        public List<string> Scopes { get; set; }
        public List<string> ScopeDescriptions { get; set; }
        public string State { get; set; }
        public string CodeChallenge { get; set; }
    }
}
