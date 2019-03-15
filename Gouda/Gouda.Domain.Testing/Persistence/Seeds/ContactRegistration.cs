using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Domain.Persistence.Seeds
{
    using Security;
    using Communication;

    public static class ContactRegistration
    {
        public static Communication.ContactRegistration[] Data => new Communication.ContactRegistration[]
        {
            new Communication.ContactRegistration() { ID = 1, ContactID = MockContact.One.ID, UserID = MockUser.One.ID, CronString = Testing.AlwaysCronString },
            new Communication.ContactRegistration() { ID = 2, ContactID = MockContact.Two.ID, UserID = MockUser.Two.ID, CronString = Testing.AlwaysCronString },
        };
    }
}
