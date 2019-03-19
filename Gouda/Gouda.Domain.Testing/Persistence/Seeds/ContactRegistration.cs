using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Domain.Persistence.Seeds
{
    using Communication;

    public static class ContactRegistration
    {
        public static Communication.ContactRegistration[] Data => MockContactRegistration.Samples();
    }
}
