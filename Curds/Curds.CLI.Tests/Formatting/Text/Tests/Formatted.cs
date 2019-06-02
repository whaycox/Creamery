using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Curds.CLI.Formatting.Text.Tests
{
    using CLI.Domain;
    using Enumerations;
    using Token.Mock;

    [TestClass]
    public class Formatted : CLI.Template.Console<Domain.Formatted>
    {
        private const int MockTokensInText = 3;

        protected override Domain.Formatted TestObject { get; } = new Domain.Formatted();

        private IToken MockToken => new IToken();
        private Mock.Formatted MockText => new Mock.Formatted(MockTokensInText);

        [TestMethod]
        public void StartsWithNoTokens()
        {
            TestObject.Write(MockConsole);
            MockConsole.VerifyOperations(0);
        }

        [TestMethod]
        public void CanBeBuiltWithText()
        {
            Domain.Formatted toTest = new Domain.Formatted(MockText);
            toTest.Write(MockConsole);
            VerificationChain verification = MockConsole.VerifyOperations(MockTokensInText);
            for (int i = 0; i < MockTokensInText; i++)
                verification.Test(ConsoleOperation.TextWritten, nameof(IToken));
        }

        [TestMethod]
        public void CanAddToken()
        {
            TestObject.Add(MockToken);
            TestObject.Write(MockConsole);
            MockConsole
                .VerifyOperations(1)
                .Test(ConsoleOperation.TextWritten, nameof(IToken));
        }

        [TestMethod]
        public void AddLineAddsNewLine()
        {
            TestObject.AddLine(MockToken);
            TestObject.Write(MockConsole);
            MockConsole
                .VerifyOperations(3)
                .Test(ConsoleOperation.TextWritten, nameof(IToken))
                .Test(ConsoleOperation.TextWritten, Environment.NewLine)
                .Test(ConsoleOperation.NewLineReset, null);
        }

        [TestMethod]
        public void WritesOutAddedTokens()
        {
            for (int i = 0; i < MockTokensInText; i++)
                TestObject.Add(MockToken);
            TestObject.Write(MockConsole);
            VerificationChain verification = MockConsole.VerifyOperations(MockTokensInText);
            for (int i = 0; i < MockTokensInText; i++)
                verification.Test(ConsoleOperation.TextWritten, nameof(IToken));
        }

        [TestMethod]
        public void CanAddText()
        {
            TestObject.Add(MockText);
            TestObject.Write(MockConsole);
            VerificationChain verification = MockConsole.VerifyOperations(MockTokensInText);
            for (int i = 0; i < MockTokensInText; i++)
                verification.Test(ConsoleOperation.TextWritten, nameof(IToken));
        }

        [TestMethod]
        public void AddLineTextAddsNewLine()
        {
            TestObject.AddLine(MockText);
            TestObject.Write(MockConsole);
            VerificationChain verification = MockConsole.VerifyOperations(MockTokensInText + 2);
            for (int i = 0; i < MockTokensInText; i++)
                verification.Test(ConsoleOperation.TextWritten, nameof(IToken));
            verification.Test(ConsoleOperation.TextWritten, Environment.NewLine);
            verification.Test(ConsoleOperation.NewLineReset, null);
        }

    }
}
