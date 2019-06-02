using System;
using System.Collections.Generic;
using System.Text;
using Curds.CLI.Formatting.Text.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.CLI.Formatting.Text.Tests
{
    using Curds.CLI.Domain;
    using Enumerations;

    [TestClass]
    public class Colored : Template.Temporary<Implementation.Colored>
    {
        private readonly ConsoleColor TestColor = ConsoleColor.Gray;

        private Implementation.Colored _obj = null;
        protected override Implementation.Colored TestObject => _obj;

        protected override int ExpectedTokens(int tokensAdded) => tokensAdded + 2;

        protected override ExpectedEvent ExpectedEngageToken => new ExpectedEvent(ConsoleOperation.TextColorApplied, TestColor);
        protected override ExpectedEvent ExpectedDisengageToken => new ExpectedEvent(ConsoleOperation.TextColorRemoved, null);
        protected override void TestWrappedToken(VerificationChain verification, int expectedTokens) => verification.Test(ConsoleOperation.TextWritten, nameof(Token.Mock.IToken));

        [TestInitialize]
        public void BuildObj()
        {
            _obj = new Implementation.Colored(ResultText, TestColor);
        }

        [TestCleanup]
        public void Dispose()
        {
            TestObject.Dispose();
        }
    }
}
