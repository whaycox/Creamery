using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Curds.Parsing.CSV.Tests
{
    [TestClass]
    public class Row : Test<Domain.Row>
    {
        protected override Domain.Row TestObject => new Mock.Row();

        [TestMethod]
        public void ExpectedRowLength() => Assert.AreEqual(Mock.Row.ExpectedLength, TestObject.RetrieveCells().Count);

        [TestMethod]
        public void DifferentCollectionSameContent()
        {
            List<Domain.Cell> cells = TestObject.RetrieveCells();
            List<Domain.Cell> otherCollection = TestObject.RetrieveCells();

            Assert.AreNotSame(otherCollection, cells);
            Assert.AreEqual(cells.Count, otherCollection.Count);

            for (int i = 0; i < cells.Count; i++)
                Assert.AreSame(cells[i], otherCollection[i]);
        }

        public void IteratorAndCollectionSameContent()
        {
            List<Domain.Cell> cells = TestObject.RetrieveCells();

            int cellToTest = 0;
            foreach (Domain.Cell cell in TestObject)
                Assert.AreSame(cells[cellToTest++], cell);
        }
    }
}
