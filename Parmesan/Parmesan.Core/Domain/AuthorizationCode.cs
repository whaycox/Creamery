using Curds.Persistence.Domain;
using System;

namespace Parmesan.Domain
{
    public class AuthorizationCode : BaseEntity
    {
        public string Code { get; set; }
        public int AuthorizationGrantID { get; set; }

        public override object[] Keys => new object[] { Code };
    }
}
