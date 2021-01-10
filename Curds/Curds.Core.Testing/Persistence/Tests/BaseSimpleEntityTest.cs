using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Curds.Persistence.Tests
{
    using Domain;

    [TestClass]
    public class BaseSimpleEntityTest
    {
        private int TestID = 109;

        private BaseSimpleEntity TestObject = new TestSimpleEntity();

        [TestInitialize]
        public void Init()
        {
            TestObject.ID = TestID;
        }

        [TestMethod]
        public void KeysAreID()
        {
            object[] actual = TestObject.Keys;

            Assert.AreEqual(1, actual.Length);
            Assert.AreEqual(TestID, actual[0]);
        }
    }
}
