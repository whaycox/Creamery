using Curds;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Feta.OpenType.Tests
{
    [TestClass]
    public class TableCollection : Test<Domain.TableCollection>
    {
        protected override Domain.TableCollection TestObject { get; } = new Domain.TableCollection();

        private Mock.Table MockTable = new Mock.Table();
        private Mock.PrimaryTable MockPrimaryTable = new Mock.PrimaryTable();

        [TestMethod]
        public void CanAddTable()
        {
            TestObject.Add(MockTable);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullTableAddedThrows()
        {
            TestObject.Add<Mock.Table>(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DuplicateTypeAddedThrows()
        {
            TestObject.Add(MockTable);
            TestObject.Add(new Mock.Table());
        }

        [TestMethod]
        public void CanRetrieveByType()
        {
            TestObject.Add(MockTable);
            Assert.AreSame(MockTable, TestObject.Retrieve<Mock.Table>());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RetrieveBeforeAddThrows()
        {
            TestObject.Retrieve<Mock.Table>();
        }

        [TestMethod]
        public void CanRetrievePrimaryTableByAddedTag()
        {
            TestObject.Add(MockPrimaryTable);
            string addedTag = MockPrimaryTable.Tag;
            MockPrimaryTable.CurrentTag = nameof(CanRetrievePrimaryTableByAddedTag);
            Assert.AreSame(MockPrimaryTable, TestObject.Retrieve(addedTag));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RetrieveWithUnknownTagThrows()
        {
            TestObject.Retrieve(MockPrimaryTable.Tag);
        }
    }
}
