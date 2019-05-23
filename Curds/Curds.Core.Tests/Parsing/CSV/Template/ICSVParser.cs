using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Curds.Parsing.CSV.Template
{
    using Domain;
    using Reader.Domain;

    public abstract class ICSVParser<T> : Test<T> where T : Abstraction.ICSVParser
    {
        private static Encoding DefaultEncoding => ReaderOptions.DefaultEncoding;
        private static Stream TextStream(string text, Encoding textEncoding) => new MemoryStream(textEncoding.GetBytes(text));

        [TestMethod]
        public void Simple()
        {
            VerifySimpleParsed(TestObject.Parse(TextStream(SimpleCSV, DefaultEncoding)));
        }
        private const string SimpleCSV = "Row1First,Row1Second,Row1Third,Row1Fourth,\r\nRow2First,Row2Second,Row2Third\r\nRow3First,Row3Second,,Row3Third\r\n,Row4First";
        protected void VerifySimpleParsed(IEnumerable<Row> rows)
        {
            List<Row> parsed = rows.ToList();
            Assert.AreEqual(4, parsed.Count);
            VerifySimpleFirst(parsed[0]);
            VerifySimpleSecond(parsed[1]);
            VerifySimpleThird(parsed[2]);
            VerifySimpleFourth(parsed[3]);
        }
        private void VerifySimpleFirst(Row first)
        {
            List<Cell> cells = first.RetrieveCells();
            Assert.AreEqual(5, cells.Count);
            Assert.AreEqual("Row1First", cells[0].Value);
            Assert.AreEqual("Row1Second", cells[1].Value);
            Assert.AreEqual("Row1Third", cells[2].Value);
            Assert.AreEqual("Row1Fourth", cells[3].Value);
            Assert.AreEqual(string.Empty, cells[4].Value);
        }
        private void VerifySimpleSecond(Row second)
        {
            List<Cell> cells = second.RetrieveCells();
            Assert.AreEqual(3, cells.Count);
            Assert.AreEqual("Row2First", cells[0].Value);
            Assert.AreEqual("Row2Second", cells[1].Value);
            Assert.AreEqual("Row2Third", cells[2].Value);
        }
        private void VerifySimpleThird(Row third)
        {
            List<Cell> cells = third.RetrieveCells();
            Assert.AreEqual(4, cells.Count);
            Assert.AreEqual("Row3First", cells[0].Value);
            Assert.AreEqual("Row3Second", cells[1].Value);
            Assert.AreEqual(string.Empty, cells[2].Value);
            Assert.AreEqual("Row3Third", cells[3].Value);
        }
        private void VerifySimpleFourth(Row fourth)
        {
            List<Cell> cells = fourth.RetrieveCells();
            Assert.AreEqual(2, cells.Count);
            Assert.AreEqual(string.Empty, cells[0].Value);
            Assert.AreEqual("Row4First", cells[1].Value);
        }


    }
}
