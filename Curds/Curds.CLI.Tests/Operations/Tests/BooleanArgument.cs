using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.CLI.Operations.Tests
{
    using Mock;

    [TestClass]
    public class BooleanArgument : Template.Console<Implementation.BooleanArgument>
    {
        private Mock.BooleanArgument _obj = new Mock.BooleanArgument();
        protected override Implementation.BooleanArgument TestObject => _obj;

        [DataTestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void SyntaxIsJustAliases(bool isRequired)
        {
            _obj.TestRequired = isRequired;
            TestObject.Syntax.Write(MockConsole);
            MockConsole
                .VerifyOperations(SyntaxExpectedOperations(isRequired))
                .VerifyBooleanArgumentSyntax(isRequired)
                .IsFinished();
        }
        private int SyntaxExpectedOperations(bool isRequired) => isRequired ? 7 : 9;

        [DataTestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void UsageIsSyntaxWithName(bool isRequired)
        {
            _obj.TestRequired = isRequired;
            TestObject.Usage.Write(MockConsole);
            MockConsole
                .VerifyOperations(UsageExpectedOperations(isRequired))
                .VerifyBooleanArgumentUsage(isRequired)
                .IsFinished();
        }
        private int UsageExpectedOperations(bool isRequired) => isRequired ? 13 : 15;

        [TestMethod]
        public void HasNoValues()
        {
            Assert.AreEqual(0, TestObject.Values.Count);
        }

        [TestMethod]
        public void ParseDoesNotConsumeAnything()
        {
            Assert.AreEqual(0, TestObject.Parse(MockArgumentCrawler).Count);
            Assert.AreEqual(0, MockArgumentCrawler.Consumptions);
        }
    }
}
