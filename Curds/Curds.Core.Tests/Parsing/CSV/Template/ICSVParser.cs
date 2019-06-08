using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Curds.Parsing.CSV.Template
{
    using Abstraction;
    using Domain;
    using Reader.Domain;

    public abstract class ICSVParser<T> : Test<T> where T : Abstraction.ICSVParser
    {
        private const char WeirdSeparator = 'þ';
        private const char WeirdQualifier = 'ÿ';

        private static Encoding DefaultEncoding => ReaderOptions.DefaultEncoding;
        private static Stream TextStream(string text, Encoding textEncoding) => new MemoryStream(textEncoding.GetBytes(text));

        private ICSVOptions BuildOptions(char? separator, char? qualifier)
        {
            CSVOptions toReturn = new CSVOptions();
            if (separator.HasValue)
                toReturn.Separator = separator.Value;
            if (qualifier.HasValue)
                toReturn.Qualifier = qualifier.Value;
            return toReturn;
        }

        protected abstract T BuildWithOptions(ICSVOptions options);

        [TestMethod]
        public void Simple()
        {
            using (Stream testStream = TextStream(SimpleCSV, DefaultEncoding))
                VerifySimpleParsed(TestObject.Parse(testStream));
        }
        private const string SimpleCSV = "Row1First,Row1Second,Row1Third,Row1Fourth,\r\nRow2First,Row2Second,Row2Third\r\nRow3First,Row3Second,,Row3Third\r\n,Row4First";
        private void VerifySimpleParsed(IEnumerable<Row> rows)
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

        [TestMethod]
        public void Separator()
        {
            using (Stream testStream = TextStream(SeparatorCSV, DefaultEncoding))
                VerifySeparatorParsed(TestObject.Parse(testStream));
        }
        private const string SeparatorCSV = "\"Row1,TheFirst\",\"Row1,TheSecond\",\"Row1,\"\"VeryQuotable\"\"TheThird\"\r\nRow2First,Row2Second,Row2Third\r\n\"Row3,\"\"VeryQuotable\"\"TheFirst\",\"Row3,TheSecond\"";
        private void VerifySeparatorParsed(IEnumerable<Row> rows)
        {
            List<Row> parsed = rows.ToList();
            Assert.AreEqual(3, parsed.Count);
            VerifySeparatorFirst(parsed[0]);
            VerifySeparatorSecond(parsed[1]);
            VerifySeparatorThird(parsed[2]);
        }
        private void VerifySeparatorFirst(Row first)
        {
            List<Cell> cells = first.RetrieveCells();
            Assert.AreEqual(3, cells.Count);
            Assert.AreEqual("Row1,TheFirst", cells[0].Value);
            Assert.AreEqual("Row1,TheSecond", cells[1].Value);
            Assert.AreEqual("Row1,\"VeryQuotable\"TheThird", cells[2].Value);
        }
        private void VerifySeparatorSecond(Row second)
        {
            List<Cell> cells = second.RetrieveCells();
            Assert.AreEqual(3, cells.Count);
            Assert.AreEqual("Row2First", cells[0].Value);
            Assert.AreEqual("Row2Second", cells[1].Value);
            Assert.AreEqual("Row2Third", cells[2].Value);
        }
        private void VerifySeparatorThird(Row third)
        {
            List<Cell> cells = third.RetrieveCells();
            Assert.AreEqual(2, cells.Count);
            Assert.AreEqual("Row3,\"VeryQuotable\"TheFirst", cells[0].Value);
            Assert.AreEqual("Row3,TheSecond", cells[1].Value);
        }

        [TestMethod]
        public void CustomSeparator()
        {
            CustomSeparatorOnly();
            CustomQualifierOnly();
            BothCustom();
        }
        private void VerifyCustomSeparator(Row parsed)
        {
            List<Cell> cells = parsed.RetrieveCells();
            Assert.AreEqual(4, cells.Count);
            Assert.AreEqual("FirstValue", cells[0].Value);
            Assert.AreEqual("SecoþndÿValue", cells[1].Value);
            Assert.AreEqual("Third\r\nValue", cells[2].Value);
            Assert.AreEqual(string.Empty, cells[3].Value);
        }

        private void CustomSeparatorOnly()
        {
            T parser = BuildWithOptions(BuildOptions(WeirdSeparator, null));
            using (Stream testStream = TextStream(SeparatorOnlyCSV, DefaultEncoding))
            {
                List<Row> parsed = parser.Parse(testStream).ToList();
                Assert.AreEqual(1, parsed.Count);
                VerifyCustomSeparator(parsed[0]);
            }
        }
        private const string SeparatorOnlyCSV = "FirstValueþ\"SecoþndÿValue\"þ\"Third\r\nValue\"þ";

        private void CustomQualifierOnly()
        {
            T parser = BuildWithOptions(BuildOptions(null, WeirdQualifier));
            using (Stream testStream = TextStream(QualifierOnlyCSV, DefaultEncoding))
            {
                List<Row> parsed = parser.Parse(testStream).ToList();
                Assert.AreEqual(1, parsed.Count);
                VerifyCustomSeparator(parsed[0]);
            }
        }
        private const string QualifierOnlyCSV = "FirstValue,ÿSecoþndÿÿValueÿ,ÿThird\r\nValueÿ,";

        private void BothCustom()
        {
            T parser = BuildWithOptions(BuildOptions(WeirdSeparator, WeirdQualifier));
            using (Stream testStream = TextStream(BothCustomCSV, DefaultEncoding))
            {
                List<Row> parsed = parser.Parse(testStream).ToList();
                Assert.AreEqual(1, parsed.Count);
                VerifyCustomSeparator(parsed[0]);
            }
        }
        private const string BothCustomCSV = "FirstValueþÿSecoþndÿÿValueÿþÿThird\r\nValueÿþ";

        [TestMethod]
        public void Large()
        {
            using (Stream testStream = Files.Manifest.LargeStream)
            {
                List<Row> parsed = TestObject.Parse(testStream).ToList();
                Assert.AreEqual(LargeExpectedRows, parsed.Count);
                Assert.IsFalse(parsed.Where(p => p.RetrieveCells().Count != LargeExpectedWidth).Any());
                int[] totals = new int[LargeExpectedWidth];
                for (int i = 0; i < LargeRowsToSum; i++)
                    AddRowToTotals(parsed[i], totals);
                CompareWithFinalRow(parsed[LargeExpectedRows - 1], totals);
            }
        }
        private const int LargeExpectedRows = 1003;
        private const int LargeExpectedWidth = 15;
        private const int LargeRowsToSum = 1000;
        private int ParseCell(Cell cell) => int.Parse(cell.Value);
        private void AddRowToTotals(Row row, int[] totals)
        {
            List<Cell> cells = row.RetrieveCells();
            for (int i = 0; i < LargeExpectedWidth; i++)
                totals[i] += ParseCell(cells[i]);
        }
        private void CompareWithFinalRow(Row final, int[] totals)
        {
            List<Cell> cells = final.RetrieveCells();
            for (int i = 0; i < LargeExpectedWidth; i++)
                Assert.AreEqual(totals[i], ParseCell(cells[i]));
        }
    }
}
