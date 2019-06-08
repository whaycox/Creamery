using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Cron.Parser.Handler.Tests
{
    using Domain;

    [TestClass]
    public class LastDayOfMonth : Template.ParsingHandler<Implementation.LastDayOfMonth>
    {
        protected override Implementation.LastDayOfMonth TestObject { get; } = new Implementation.LastDayOfMonth(null);

        protected override Implementation.LastDayOfMonth Build(ParsingHandler successor) => new Implementation.LastDayOfMonth(successor);

        private string LastDayOfMonthString(int? offset) => $"L{(offset.HasValue ? $"-{offset}" : string.Empty)}";

        [TestMethod]
        public void ParsesWithoutOffset()
        {
            TestParse<Range.Implementation.LastDayOfMonth>(LastDayOfMonthString(null), ParsesWithoutOffsetHelper);
            TestParse<Range.Implementation.LastDayOfMonth>(LastDayOfMonthString(0), ParsesWithoutOffsetHelper);
        }
        private void ParsesWithoutOffsetHelper(Range.Implementation.LastDayOfMonth parsed)
        {
            Assert.AreEqual(0, parsed.Offset);
        }

        [TestMethod]
        public void ParsesWithOffset()
        {
            for (int i = 1; i <= Range.Implementation.LastDayOfMonth.MaxOffset; i++)
                TestParse<Range.Implementation.LastDayOfMonth>(LastDayOfMonthString(i), (p) => ParsesWithOffsetHelper(p, i));
        }
        private void ParsesWithOffsetHelper(Range.Implementation.LastDayOfMonth parsed, int expected)
        {
            Assert.AreEqual(expected, parsed.Offset);
        }
    }
}
