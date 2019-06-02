using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.CLI.Formatting.Token.Tests
{
    [TestClass]
    public class EndConcatenation : Template.PlainText<Implementation.EndConcatenation>
    {
        protected override Implementation.EndConcatenation BuildWithText(string testText) => new Implementation.EndConcatenation(testText);
    }
}
