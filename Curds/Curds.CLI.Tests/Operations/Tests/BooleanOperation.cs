using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.CLI.Operations.Tests
{
    using Mock;

    [TestClass]
    public class BooleanOperation : Template.Console<Implementation.BooleanOperation>
    {
        private Mock.BooleanOperation _obj = new Mock.BooleanOperation();
        protected override Implementation.BooleanOperation TestObject => _obj;

        [TestMethod]
        public void SyntaxIsJustAliases()
        {
            TestObject.Syntax.Write(MockConsole);
            MockConsole
                .VerifyOperations(7)
                .VerifyBooleanOperationSyntax()
                .IsFinished();
        }

        [TestMethod]
        public void UsageIsSyntaxAndName()
        {
            TestObject.Usage.Write(MockConsole);
            MockConsole
                .VerifyOperations(13)
                .VerifyBooleanOperationUsage()
                .IsFinished();
        }

        [TestMethod]
        public void ParseConsumesNothing()
        {
            var parsed = TestObject.Parse(MockArgumentCrawler);
            Assert.AreEqual(0, parsed.Count);
            Assert.AreEqual(0, MockArgumentCrawler.Consumptions);
        }
    }
}
