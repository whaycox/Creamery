using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain.Persistence;
using Gouda.Domain.Communication;
using Gouda.Domain.Security;

namespace Gouda.Domain.Communication.Contacts
{
    public class MockContactOne : Contact
    {
        public override Entity Clone() => CloneInternal(new MockContactOne());

        public static MockContactOne Sample => new MockContactOne()
        {
            ID = 1,
            UserID = MockUser.One.ID,
            Name = nameof(MockContactOne),
            CronString = Testing.AlwaysCronString,
        };
    }
}
