using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;


namespace Curds.CLI.Formatting.Token.Tests
{
    using Enumerations;

    [TestClass]
    public class SetTextColor : CLI.Template.Console<Implementation.SetTextColor>
    {
        private const ConsoleColor TestColor = ConsoleColor.DarkCyan;

        protected override Implementation.SetTextColor TestObject { get; } = new Implementation.SetTextColor(TestColor);

        [TestMethod]
        public void AppliesColor()
        {
            TestObject.Write(MockConsole);
            MockConsole
                .VerifyOperations(1)
                .Test(ConsoleOperation.TextColorApplied, TestColor);
        }
    }
}
