using System.Collections.Generic;

namespace Parmesan.Server.ViewModels.Domain
{
    public class AuthorizationRequestViewModel
    {
        public string TicketNumber { get; set; }
        public string ClientDisplayName { get; set; }
        public List<string> ScopeDescriptions { get; set; }
    }
}
