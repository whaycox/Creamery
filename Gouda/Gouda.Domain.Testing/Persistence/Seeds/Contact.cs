using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Domain.Persistence.Seeds
{
    public static class Contact
    {
        public static Communication.Contact[] Data => Communication.MockContact.Samples;
    }
}
