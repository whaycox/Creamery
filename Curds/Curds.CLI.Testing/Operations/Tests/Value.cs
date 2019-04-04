using Curds.Domain.CLI.Operations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Curds.CLI.Operations.Tests
{
    using Curds.Domain.CLI;
    using Formatting;

    [TestClass]
    public class Value : OptionValueTemplate<Operations.Value>
    {
        protected override Operations.Value TestObject { get; } = new MockValue();

        protected override int SyntaxExpectedWrites => 5;
        protected override void VerifySyntax(MockConsoleWriter writer) =>
            writer.StartsWith(NewLine(true))
                .ThenHas(NewLine(false))
                .ThenHas(Operations.Value.SyntaxStart)
                .ThenHas(nameof(MockValue))
                .ThenHas(Operations.Value.SyntaxEnd);

        protected override int UsageExpectedWrites => 6;
        protected override void VerifyUsage(MockConsoleWriter writer) =>
            writer.StartsWith(NewLine(true))
                .ThenHas(TextColor(CLIEnvironment.Value))
                .ThenHas(NewLine(false))
                .ThenHas(nameof(MockValue))
                .ThenHas(TextColor(CLIEnvironment.DefaultTextColor))
                .ThenHas($": {nameof(MockValue)}{nameof(MockValue.Description)}");

        [TestMethod]
        public void ParseSetsRawCurrentValue()
        {
            ArgumentCrawler crawler = new ArgumentCrawler(new string[] { nameof(ParseSetsRawCurrentValue) });
            Assert.IsNull(TestObject.RawValue);
            TestObject.Parse(crawler);
            Assert.AreEqual(nameof(ParseSetsRawCurrentValue), TestObject.RawValue);
        }
    }
}
