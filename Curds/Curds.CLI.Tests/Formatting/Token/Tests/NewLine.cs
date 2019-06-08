using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Curds.CLI.Formatting.Token.Tests
{
    using Enumerations;

    [TestClass]
    public class NewLine : CLI.Template.Console<Implementation.NewLine>
    {
        protected override Implementation.NewLine TestObject { get; } = new Implementation.NewLine();

        [TestMethod]
        public void WritesANewLineAndResets()
        {
            TestObject.Write(MockConsole);
            MockConsole
                .VerifyOperations(2)
                .Test(ConsoleOperation.TextWritten, Environment.NewLine)
                .Test(ConsoleOperation.NewLineReset, null);
        }
    }
}
