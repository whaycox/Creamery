using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Domain.Communication
{
    using Contacts;

    public static class MockContact
    {
        public static IEnumerable<Contact> Samples => new List<Contact>()
        {
            { MockContactOne.Sample },
            { MockContactTwo.Sample },
        };
    }
}
