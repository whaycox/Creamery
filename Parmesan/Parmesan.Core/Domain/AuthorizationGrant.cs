using System;
using System.Collections.Generic;
using System.Text;
using Curds.Persistence.Domain;

namespace Parmesan.Domain
{
    public class AuthorizationGrant : BaseSimpleEntity
    {
        public int ClientID { get; set; }
        public int UserID { get; set; }
        public string Scope { get; set; }
        public string RedirectUri { get; set; }
        public string CodeChallenge { get; set; }
        public DateTimeOffset Expiration { get; set; }
    }
}
