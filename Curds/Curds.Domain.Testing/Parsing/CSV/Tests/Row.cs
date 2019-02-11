using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Curds.Domain;

namespace Curds.Domain.Parsing.CSV.Tests
{
    [TestClass]
    public class Row : TestTemplate<CSV.Row>
    {
        protected override CSV.Row TestObject => new MockRow();

        [TestMethod]
        public void ExpectedRowLength() => Assert.AreEqual(MockRow.ExpectedLength, TestObject.RetrieveCells().Count);

        [TestMethod]
        public void DifferentCollectionSameContent()
        {
            List<CSV.Cell> cells = TestObject.RetrieveCells();
            List<CSV.Cell> otherCollection = TestObject.RetrieveCells();

            Assert.AreNotSame(otherCollection, cells);
            Assert.AreEqual(cells.Count, otherCollection.Count);

            for (int i = 0; i < cells.Count; i++)
                Assert.AreSame(cells[i], otherCollection[i]);
        }

        public void IteratorAndCollectionSameContent()
        {
            List<CSV.Cell> cells = TestObject.RetrieveCells();

            int cellToTest = 0;
            foreach (CSV.Cell cell in TestObject)
                Assert.AreSame(cells[cellToTest++], cell);
        }
    }
}
