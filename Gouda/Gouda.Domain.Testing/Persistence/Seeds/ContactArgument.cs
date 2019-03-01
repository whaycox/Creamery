using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Domain.Persistence.Seeds
{
    public static class ContactArgument
    {
        public static Communication.ContactArgument[] Data => Communication.MockContactArgument.Samples;
    }
}
