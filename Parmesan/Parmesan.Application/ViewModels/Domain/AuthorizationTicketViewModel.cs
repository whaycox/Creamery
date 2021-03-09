using System;
using System.Collections.Generic;
using System.Text;

namespace Parmesan.Application.ViewModels.Domain
{
    public class AuthorizationTicketViewModel
    {
        public string ClientDisplayName { get; set; }
        public string TicketNumber { get; set; }
        public List<string> ScopeDescriptions { get; } = new List<string>();
    }
}
