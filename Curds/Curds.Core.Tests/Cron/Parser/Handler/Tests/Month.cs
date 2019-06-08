using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Cron.Parser.Handler.Tests
{
    using Domain;

    [TestClass]
    public class Month : Template.Definite<Implementation.Month>
    {
        protected override Implementation.Month TestObject { get; } = new Implementation.Month(null);

        protected override Implementation.Month Build(ParsingHandler successor) => new Implementation.Month(successor);

        [TestMethod]
        public void ThrowsWithBadNames()
        {
            Assert.ThrowsException<FormatException>(() => TestObject.HandleParse("JON"));
            Assert.ThrowsException<FormatException>(() => TestObject.HandleParse("FAB"));
            Assert.ThrowsException<FormatException>(() => TestObject.HandleParse("MER"));
            Assert.ThrowsException<FormatException>(() => TestObject.HandleParse("API"));
            Assert.ThrowsException<FormatException>(() => TestObject.HandleParse("MEY"));
            Assert.ThrowsException<FormatException>(() => TestObject.HandleParse("JUNE"));
            Assert.ThrowsException<FormatException>(() => TestObject.HandleParse("JULY"));
            Assert.ThrowsException<FormatException>(() => TestObject.HandleParse("AAG"));
            Assert.ThrowsException<FormatException>(() => TestObject.HandleParse("SAP"));
            Assert.ThrowsException<FormatException>(() => TestObject.HandleParse("OTC"));
            Assert.ThrowsException<FormatException>(() => TestObject.HandleParse("NAV"));
            Assert.ThrowsException<FormatException>(() => TestObject.HandleParse("DOC"));
        }

        [TestMethod]
        public void CanLookupFromNames()
        {
            for (int i = Token.Domain.Month.MinMonth; i <= Token.Domain.Month.MaxMonth; i++)
            {
                switch (i)
                {
                    case 1:
                        TestName("JAN", i);
                        break;
                    case 2:
                        TestName("FEB", i);
                        break;
                    case 3:
                        TestName("MAR", i);
                        break;
                    case 4:
                        TestName("APR", i);
                        break;
                    case 5:
                        TestName("MAY", i);
                        break;
                    case 6:
                        TestName("JUN", i);
                        break;
                    case 7:
                        TestName("JUL", i);
                        break;
                    case 8:
                        TestName("AUG", i);
                        break;
                    case 9:
                        TestName("SEP", i);
                        break;
                    case 10:
                        TestName("OCT", i);
                        break;
                    case 11:
                        TestName("NOV", i);
                        break;
                    case 12:
                        TestName("DEC", i);
                        break;
                }
            }
        }
        private void TestName(string name, int expected)
        {
            TestParse<Range.Domain.Basic>(name, (p) => VerifyParsedName(p, expected));
        }
        private void VerifyParsedName(Range.Domain.Basic parsed, int expected)
        {
            Assert.AreEqual(expected, parsed.Min);
            Assert.AreEqual(expected, parsed.Max);
        }

    }
}
