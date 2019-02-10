using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain;
using System.IO;
using Curds.Domain.Parsing.CSV;
using Curds.Application.Parsing.CSV;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Infrastructure.Parsing.CSV
{
    public abstract class ParsingTest
    {

        protected void TestParsingCase(ParsingCase testCase)
        {
            try
            {
                List<Row> parsedResults = Parse(testCase.Source, testCase.Options);
                Assert.AreEqual(testCase.ExpectedRowCount, parsedResults.Count);
                for (int i = 0; i < parsedResults.Count; i++)
                {
                    List<Cell> rowCells = parsedResults[i].RetrieveCells();
                    Assert.AreEqual(rowCells.Count, testCase.ExpectedRowLength(i));
                    testCase.VerifyContent(i, rowCells);
                }
            }
            finally
            {
                testCase.Source.Dispose();
            }
        }
        private List<Row> Parse(Stream textStream, ReadingOptions options) => BuildParser(options).Parse(textStream).ToList();

        protected abstract IParser BuildParser(ReadingOptions options);



    }
}
