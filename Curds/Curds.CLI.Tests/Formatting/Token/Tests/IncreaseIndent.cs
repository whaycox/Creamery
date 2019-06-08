using System;
using System.Collections.Generic;
using System.Text;
using Curds.CLI.Formatting.Token.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.CLI.Formatting.Token.Tests
{
    using Enumerations;

    [TestClass]
    public class IncreaseIndent : CLI.Template.Console<Implementation.IncreaseIndent>
    {
        protected override Implementation.IncreaseIndent TestObject { get; } = new Implementation.IncreaseIndent();

        [TestMethod]
        public void IncreasesIndent()
        {
            TestObject.Write(MockConsole);
            MockConsole
                .VerifyOperations(1)
                .Test(ConsoleOperation.IndentsIncreased, null);
        }
    }
}
