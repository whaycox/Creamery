using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Domain.Parsing.CSV.Tests
{
    [TestClass]
    public class Row
    {
        private MockRow MockRow = new MockRow();

        [TestMethod]
        public void ExpectedRowLength() => Assert.AreEqual(MockRow.ExpectedLength, MockRow.RetrieveCells().Count);

        [TestMethod]
        public void DifferentCollectionSameContent()
        {
            List<Cell> cells = MockRow.RetrieveCells();
            List<Cell> otherCollection = MockRow.RetrieveCells();

            Assert.AreNotSame(otherCollection, cells);
            Assert.AreEqual(cells.Count, otherCollection.Count);

            for (int i = 0; i < cells.Count; i++)
                Assert.AreSame(cells[i], otherCollection[i]);
        }

        public void IteratorAndCollectionSameContent()
        {
            List<Cell> cells = MockRow.RetrieveCells();

            int cellToTest = 0;
            foreach (Cell cell in MockRow)
                Assert.AreSame(cells[cellToTest++], cell);
        }
    }
}
