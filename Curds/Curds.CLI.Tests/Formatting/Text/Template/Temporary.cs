using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.CLI.Formatting.Text.Template
{
    using Enumerations;
    using Token.Mock;
    using CLI.Domain;

    public abstract class Temporary<T> : CLI.Template.Console<T> where T : Domain.Temporary
    {
        protected Domain.Formatted ResultText = new Domain.Formatted();

        protected IToken MockToken => new IToken();

        protected abstract ExpectedEvent ExpectedEngageToken { get; }
        protected abstract ExpectedEvent ExpectedDisengageToken { get; }

        [TestMethod]
        public void WritesNothingAtFirst()
        {
            TestObject.Write(MockConsole);
            Assert.AreEqual(0, MockConsole.Operations.Count);
        }

        [TestMethod]
        public void AddingATokenAddsAnEngage()
        {
            TestObject.Add(MockToken);
            TestObject.Write(MockConsole);
            MockConsole
                .VerifyOperations(2)
                .Test(ExpectedEngageToken)
                .Test(ConsoleOperation.TextWritten, nameof(IToken));
        }

        [TestMethod]
        public void DisposingAddsAnEngageAndDisengage()
        {
            TestObject.Dispose();
            TestObject.Write(MockConsole);
            MockConsole
                .VerifyOperations(2)
                .Test(ExpectedEngageToken)
                .Test(ExpectedDisengageToken);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(4)]
        [DataRow(5)]
        [DataRow(6)]
        [DataRow(7)]
        public void ProperlyWrapsNTokens(int tokensToAdd)
        {
            for (int i = 0; i < tokensToAdd; i++)
                TestObject.Add(MockToken);
            TestObject.Dispose();
            TestObject.Write(MockConsole);

            int expectedTokens = ExpectedTokens(tokensToAdd);
            VerificationChain verification = MockConsole.VerifyOperations(expectedTokens);
            while (verification.CurrentIndex < expectedTokens)
            {
                if (verification.CurrentIndex == 0)
                    verification.Test(ExpectedEngageToken);
                else if (verification.CurrentIndex == expectedTokens - 1)
                    verification.Test(ExpectedDisengageToken);
                else
                    TestWrappedToken(verification, expectedTokens);
            }
        }
        protected abstract int ExpectedTokens(int tokensAdded);
        protected abstract void TestWrappedToken(VerificationChain verification, int expectedTokens);

        [TestMethod]
        public void DisposingAppendsToParent()
        {
            TestObject.Dispose();
            ResultText.Write(MockConsole);
            MockConsole
                .VerifyOperations(2)
                .Test(ExpectedEngageToken)
                .Test(ExpectedDisengageToken);
        }
    }
}
