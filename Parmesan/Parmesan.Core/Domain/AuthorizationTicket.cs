using System;
using System.Collections.Generic;
using System.Text;
using Curds.Persistence.Domain;

namespace Parmesan.Domain
{
    public class AuthorizationTicket : BaseEntity
    {
        public string TicketNumber { get; set; }

        public override object[] Keys => new object[] { TicketNumber };
    }
}
