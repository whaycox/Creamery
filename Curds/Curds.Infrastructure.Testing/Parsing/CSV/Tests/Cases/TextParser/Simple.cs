using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain.Parsing.CSV;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Infrastructure.Parsing.CSV.Tests.Cases.TextParser
{
    public class Simple : ParsingCase
    {
        private const string SimpleCSV = "Row1First,Row1Second,Row1Third,Row1Fourth,\r\nRow2First,Row2Second,Row2Third\r\nRow3First,Row3Second,,Row3Third\r\n,Row4First";

        public override ReadingOptions Options => null;

        public override int ExpectedRowCount => 4;

        public Simple()
            : base(PrepareTextStream(SimpleCSV, Encoding.Default))
        { }

        public override int ExpectedRowLength(int rowIndex)
        {
            switch (rowIndex)
            {
                case 0:
                    return 5;
                case 1:
                    return 3;
                case 2:
                    return 4;
                case 3:
                    return 2;
                default:
                    throw new InvalidOperationException("Unexpected row");
            }
        }

        public override void VerifyContent(int rowIndex, List<Cell> cells)
        {
            switch (rowIndex)
            {
                case 0:
                    Assert.AreEqual("Row1First", cells[0].Value);
                    Assert.AreEqual("Row1Second", cells[1].Value);
                    Assert.AreEqual("Row1Third", cells[2].Value);
                    Assert.AreEqual("Row1Fourth", cells[3].Value);
                    Assert.AreEqual(string.Empty, cells[4].Value);
                    break;
                case 1:
                    Assert.AreEqual("Row2First", cells[0].Value);
                    Assert.AreEqual("Row2Second", cells[1].Value);
                    Assert.AreEqual("Row2Third", cells[2].Value);
                    break;
                case 2:
                    Assert.AreEqual("Row3First", cells[0].Value);
                    Assert.AreEqual("Row3Second", cells[1].Value);
                    Assert.AreEqual(string.Empty, cells[2].Value);
                    Assert.AreEqual("Row3Third", cells[3].Value);
                    break;
                case 3:
                    Assert.AreEqual(string.Empty, cells[0].Value);
                    Assert.AreEqual("Row4First", cells[1].Value);
                    break;
            }
        }
    }
}
