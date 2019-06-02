using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Curds.CLI.Formatting.Text.Tests
{
    using Curds.CLI.Domain;
    using Enumerations;
    using Token.Mock;

    [TestClass]
    public class Concatenated : Template.Temporary<Implementation.Concatenated>
    {
        private const string TestStart = nameof(TestStart);
        private const string TestBetween = nameof(TestBetween);
        private const string TestEnd = nameof(TestEnd);

        private Implementation.Concatenated _obj = null;
        protected override Implementation.Concatenated TestObject => _obj;

        protected override int ExpectedTokens(int tokensAdded) => tokensAdded + 2 + (tokensAdded - 1); //Two for the start and end, and n - 1 for the between

        protected override ExpectedEvent ExpectedEngageToken => new ExpectedEvent(ConsoleOperation.TextWritten, TestStart);
        protected override ExpectedEvent ExpectedDisengageToken => new ExpectedEvent(ConsoleOperation.TextWritten, TestEnd);
        protected override void TestWrappedToken(VerificationChain verification, int expectedTokens)
        {
            verification.Test(ConsoleOperation.TextWritten, nameof(Token.Mock.IToken));
            if (verification.CurrentIndex != expectedTokens - 1)
                verification.Test(ConsoleOperation.TextWritten, TestBetween);
        }

        [TestInitialize]
        public void BuildObj()
        {
            _obj = new Implementation.Concatenated(ResultText, TestStart, TestBetween, TestEnd);
        }

        [TestCleanup]
        public void Dispose()
        {
            TestObject.Dispose();
        }

        [TestMethod]
        public void CanConcatenateWithNewLineBetween()
        {
            _obj = new Implementation.Concatenated(ResultText, TestStart, new Token.Implementation.NewLine(), TestEnd);
            for (int i = 0; i < 3; i++)
                TestObject.Add(MockToken);
            TestObject.Dispose();
            TestObject.Write(MockConsole);
            MockConsole
                .VerifyOperations(9)
                .Test(ConsoleOperation.TextWritten, TestStart)
                .Test(ConsoleOperation.TextWritten, nameof(IToken))
                .Test(ConsoleOperation.TextWritten, Environment.NewLine)
                .Test(ConsoleOperation.NewLineReset, null)
                .Test(ConsoleOperation.TextWritten, nameof(IToken))
                .Test(ConsoleOperation.TextWritten, Environment.NewLine)
                .Test(ConsoleOperation.NewLineReset, null)
                .Test(ConsoleOperation.TextWritten, nameof(IToken))
                .Test(ConsoleOperation.TextWritten, TestEnd)
                .IsFinished();
        }
    }
}
