using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain.Persistence;
using Gouda.Domain.Communication;
using Gouda.Domain.Security;

namespace Gouda.Domain.Communication.Contacts
{
    public class MockContactTwo : Contact
    {
        public override Entity Clone() => CloneInternal(new MockContactTwo());

        public static MockContactTwo Sample => new MockContactTwo()
        {
            ID = 2,
            UserID = MockUser.Two.ID,
            Name = nameof(MockContactTwo),
            CronString = Testing.AlwaysCronString,
        };
    }
}
