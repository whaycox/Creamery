using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Curds.Domain.Parsing.CSV;
using Curds.Application.Parsing.CSV;
using Curds.Domain;

namespace Curds.Infrastructure.Parsing.CSV.Tests
{
    [TestClass]
    public class TextParser : ParsingTest
    {
        protected override IParser BuildParser(ReadingOptions options)
        {
            if (options == null)
                return new CSV.TextParser();
            else
                return new CSV.TextParser(options);
        }

        [TestMethod]
        public void CustomSeparator()
        {
            TestParsingCase(Cases.TextParser.CustomSeparator.DelimiterOnly);
            TestParsingCase(Cases.TextParser.CustomSeparator.SeparatorOnly);
            TestParsingCase(Cases.TextParser.CustomSeparator.Both);
        }

        [TestMethod]
        public void Large()
        {
            TestParsingCase(new Cases.TextParser.Large());
        }

        [TestMethod]
        public void Separator()
        {
            TestParsingCase(new Cases.TextParser.Separator());
        }

        [TestMethod]
        public void Simple()
        {
            TestParsingCase(new Cases.TextParser.Simple());
        }

        [TestMethod]
        public void SupplyEncodings()
        {
            TestParsingCase(Cases.TextParser.SupplyEncodings.ANSI);
            TestParsingCase(Cases.TextParser.SupplyEncodings.UTF8);
            TestParsingCase(Cases.TextParser.SupplyEncodings.UCS2BE);
            TestParsingCase(Cases.TextParser.SupplyEncodings.UCS2LE);
        }

    }
}
