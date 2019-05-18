using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Cron.Parser.Handler.Template
{
    public abstract class Definite<T> : ParsingHandler<T> where T : Implementation.Definite
    {
        private string DefiniteString(int min, int? max) => $"{min}{(max.HasValue ? $"-{max}" : string.Empty)}";

        [TestMethod]
        public void InvalidStringsThrow()
        {
            Assert.ThrowsException<FormatException>(() => TestObject.HandleParse("NOT-REAL"));
            Assert.ThrowsException<FormatException>(() => TestObject.HandleParse("-1-2"));
            Assert.ThrowsException<FormatException>(() => TestObject.HandleParse("0--1"));
            Assert.ThrowsException<FormatException>(() => TestObject.HandleParse("2-1"));
        }

        [TestMethod]
        public void ParsesARangeOfValues()
        {
            TestParse<Range.Domain.Basic>(DefiniteString(RangeOfValuesMin, RangeOfValuesMax), ParsesARangeOfValuesHelper);
        }
        private const int RangeOfValuesMin = 1;
        private const int RangeOfValuesMax = 10;
        private void ParsesARangeOfValuesHelper(Range.Domain.Basic parsed)
        {
            Assert.AreEqual(RangeOfValuesMin, parsed.Min);
            Assert.AreEqual(RangeOfValuesMax, parsed.Max);
        }

        [TestMethod]
        public void ParsesASingleValue()
        {
            TestParse<Range.Domain.Basic>(DefiniteString(SingleValue, null), ParsesASingleValueHelper);
        }
        private const int SingleValue = 13;
        private void ParsesASingleValueHelper(Range.Domain.Basic parsed)
        {
            Assert.AreEqual(SingleValue, parsed.Min);
            Assert.AreEqual(SingleValue, parsed.Max);
        }
    }
}
