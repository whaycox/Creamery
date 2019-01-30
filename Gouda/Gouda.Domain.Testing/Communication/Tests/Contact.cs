using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gouda.Domain.Communication.Tests
{
    [TestClass]
    public class Contact : ContactTemplate<MockContactOne>
    {
        protected override MockContactOne Sample => new MockContactOne() { ID = 5, UserID = 2, Name = nameof(MockContactOne), CronString = Testing.AlwaysCronString };
    }
}
