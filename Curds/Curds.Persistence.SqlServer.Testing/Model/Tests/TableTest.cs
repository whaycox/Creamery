using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Curds.Persistence.Model.Tests
{
    using Domain;

    [TestClass]
    public class TableTest
    {
        private Column TestColumnOne = new Column { Name = nameof(TestColumnOne) };
        private Column TestColumnTwo = new Column { Name = nameof(TestColumnTwo) };

        private Table TestObject = new Table();

        [TestMethod]
        public void IdentityColumnReturnsFirstIdentity()
        {
            TestColumnTwo.IsIdentity = true;
            TestObject.Columns.Add(TestColumnOne);
            TestObject.Columns.Add(TestColumnTwo);

            Column actual = TestObject.IdentityColumn;

            Assert.AreEqual(TestColumnTwo, actual);
        }

        [TestMethod]
        public void IdentityColumnNullIfNone()
        {
            TestObject.Columns.Add(TestColumnOne);
            TestObject.Columns.Add(TestColumnTwo);

            Column actual = TestObject.IdentityColumn;

            Assert.IsNull(actual);
        }
    }
}
