using System.Collections.Generic;

namespace Parmesan.ViewModels.Domain
{
    public class AuthorizationRequestViewModel
    {
        public string PublicClientID { get; set; }
        public string ClientDisplayName { get; set; }
        public string RedirectUri { get; set; }
        public List<string> ScopeDescriptions { get; set; }
        public string State { get; set; }
        public string CodeChallenge { get; set; }
    }
}
