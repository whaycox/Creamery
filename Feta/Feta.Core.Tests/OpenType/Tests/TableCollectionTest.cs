using Curds;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Feta.OpenType.Tests
{
    using Implementation;
    using Mock;

    [TestClass]
    public class TableCollectionTest : Test<TableCollection>
    {
        protected override TableCollection TestObject { get; } = new TableCollection();

        private MockTable MockTable = new MockTable();
        private MockPrimaryTable MockPrimaryTable = new MockPrimaryTable();

        [TestMethod]
        public void CanAddTable()
        {
            TestObject.Add(MockTable);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullTableAddedThrows()
        {
            TestObject.Add<MockTable>(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DuplicateTypeAddedThrows()
        {
            TestObject.Add(MockTable);
            TestObject.Add(new MockTable());
        }

        [TestMethod]
        public void CanRetrieveByType()
        {
            TestObject.Add(MockTable);
            Assert.AreSame(MockTable, TestObject.Retrieve<MockTable>());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RetrieveBeforeAddThrows()
        {
            TestObject.Retrieve<MockTable>();
        }

        [TestMethod]
        public void CanRetrievePrimaryTableByAddedTag()
        {
            TestObject.Add(MockPrimaryTable);
            Assert.AreSame(MockPrimaryTable, TestObject.Retrieve(MockPrimaryTable.Tag));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RetrieveWithUnknownTagThrows()
        {
            TestObject.Retrieve(MockPrimaryTable.Tag);
        }
    }
}
