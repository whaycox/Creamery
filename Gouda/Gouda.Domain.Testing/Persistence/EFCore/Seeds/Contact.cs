using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Domain.Persistence.EFCore.Seeds
{
    public static class Contact
    {
        public static Communication.Contact[] Data => Communication.MockContact.Samples;
    }
}
