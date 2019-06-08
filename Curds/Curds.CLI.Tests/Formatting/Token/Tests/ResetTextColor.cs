using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.CLI.Formatting.Token.Tests
{
    using Enumerations;

    [TestClass]
    public class ResetTextColor : CLI.Template.Console<Implementation.ResetTextColor>
    {
        protected override Implementation.ResetTextColor TestObject { get; } = new Implementation.ResetTextColor();

        [TestMethod]
        public void RemovesColor()
        {
            TestObject.Write(MockConsole);
            MockConsole
                .VerifyOperations(1)
                .Test(ConsoleOperation.TextColorRemoved, null);
        }
    }
}
