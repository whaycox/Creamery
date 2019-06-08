using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.CLI.Formatting.Token.Tests
{
    using Enumerations;

    [TestClass]
    public class DecreaseIndent : CLI.Template.Console<Implementation.DecreaseIndent>
    {
        protected override Implementation.DecreaseIndent TestObject { get; } = new Implementation.DecreaseIndent();

        [TestMethod]
        public void DecreasesConsoleIndents()
        {
            TestObject.Write(MockConsole);
            MockConsole
                .VerifyOperations(1)
                .Test(ConsoleOperation.IndentsDecreased, null);
        }
    }
}
