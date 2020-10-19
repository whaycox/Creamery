using Curds.Persistence.Domain;
using System;

namespace Parmesan.Domain
{
    public class AuthorizationCode : BaseEntity
    {
        public string Code { get; set; }
        public int ClientID { get; set; }
        public int UserID { get; set; }
        public string Scope { get; set; }
        public string RedirectUri { get; set; }
        public string CodeChallenge { get; set; }
        public DateTimeOffset Expiration { get; set; }

        public override object[] Keys => new object[] { Code };
    }
}
