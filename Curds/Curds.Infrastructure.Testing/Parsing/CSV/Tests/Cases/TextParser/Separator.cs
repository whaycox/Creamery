using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Curds.Domain.Parsing.CSV;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Infrastructure.Parsing.CSV.Tests.Cases.TextParser
{
    using TestFiles;

    public class Separator : ParsingCase
    {
        private const string SeparatorCSV = "\"Row1,TheFirst\",\"Row1,TheSecond\",\"Row1,\"\"VeryQuotable\"\"TheThird\"\r\nRow2First,Row2Second,Row2Third\r\n\"Row3,\"\"VeryQuotable\"\"TheFirst\",\"Row3,TheSecond\"";

        public override ReadingOptions Options => new ReadingOptions();

        public override int ExpectedRowCount => 3;

        public Separator()
            : base(PrepareTextStream(SeparatorCSV, Encoding.Default))
        { }

        public override int ExpectedRowLength(int rowIndex)
        {
            switch (rowIndex)
            {
                case 0:
                    return 3;
                case 1:
                    return 3;
                case 2:
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
                    Assert.AreEqual("Row1,TheFirst", cells[0].Value);
                    Assert.AreEqual("Row1,TheSecond", cells[1].Value);
                    Assert.AreEqual("Row1,\"VeryQuotable\"TheThird", cells[2].Value);
                    break;
                case 1:
                    Assert.AreEqual("Row2First", cells[0].Value);
                    Assert.AreEqual("Row2Second", cells[1].Value);
                    Assert.AreEqual("Row2Third", cells[2].Value);
                    break;
                case 2:
                    Assert.AreEqual("Row3,\"VeryQuotable\"TheFirst", cells[0].Value);
                    Assert.AreEqual("Row3,TheSecond", cells[1].Value);
                    break;
            }
        }
    }
}
