using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gouda.Domain.Communication.Tests
{
    using Contacts;

    [TestClass]
    public class Contact : ContactTemplate<MockContactOne>
    {
        protected override MockContactOne Sample => MockContactOne.Sample;
    }
}
