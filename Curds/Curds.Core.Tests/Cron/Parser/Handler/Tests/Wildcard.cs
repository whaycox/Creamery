using Curds.Cron.Parser.Handler.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Curds.Cron.Parser.Handler.Tests
{
    [TestClass]
    public class Wildcard : Template.ParsingHandler<Implementation.Wildcard>
    {
        private string WildcardString(int? denominator) => $"*{(denominator.HasValue ? $"/{denominator}" : string.Empty)}";

        protected override Implementation.Wildcard TestObject { get; } = new Implementation.Wildcard(null);

        protected override Implementation.Wildcard Build(ParsingHandler successor) => new Implementation.Wildcard(successor);

        [TestMethod]
        public void InvalidStringsThrow()
        {
            Assert.ThrowsException<FormatException>(() => TestObject.HandleParse(WildcardString(0)));
            Assert.ThrowsException<FormatException>(() => TestObject.HandleParse(WildcardString(-1)));
            Assert.ThrowsException<FormatException>(() => TestObject.HandleParse("/*"));
            Assert.ThrowsException<FormatException>(() => TestObject.HandleParse("*/*"));
        }

        [TestMethod]
        public void NoDenominatorIsUnbounded()
        {
            TestParse<Range.Implementation.Unbounded>(WildcardString(null), DoNothing);
        }


        [TestMethod]
        public void DenominatorIsStep()
        {
            TestParse<Range.Implementation.Step>(WildcardString(TestDenominator), DenominatorIsStepHelper);
        }
        private const int TestDenominator = 1;
        private void DenominatorIsStepHelper(Range.Implementation.Step parsed)
        {
            Assert.AreEqual(TestDenominator, parsed.StepValueFromMin);
        }
    }
}
