using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.CLI.Formatting.Text.Tests
{
    using Curds.CLI.Domain;
    using Enumerations;
    using Token.Mock;

    [TestClass]
    public class Indented : Template.Temporary<Implementation.Indented>
    {
        private Implementation.Indented _obj = null;
        protected override Implementation.Indented TestObject => _obj;

        protected override int ExpectedTokens(int tokensAdded) => tokensAdded + 2;

        protected override ExpectedEvent ExpectedEngageToken => new ExpectedEvent(ConsoleOperation.IndentsIncreased, null);
        protected override ExpectedEvent ExpectedDisengageToken => new ExpectedEvent(ConsoleOperation.IndentsDecreased, null);
        protected override void TestWrappedToken(VerificationChain verification, int expectedTokens) => verification.Test(ConsoleOperation.TextWritten, nameof(IToken));

        [TestInitialize]
        public void BuildObj()
        {
            _obj = new Implementation.Indented(ResultText);
        }
    }
}
