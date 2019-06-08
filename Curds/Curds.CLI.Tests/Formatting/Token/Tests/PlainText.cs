using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.CLI.Formatting.Token.Tests
{
    using Curds.CLI.Formatting.Token.Implementation;
    using Enumerations;

    [TestClass]
    public class PlainText : Template.PlainText<Implementation.PlainText>
    {
        protected override Implementation.PlainText BuildWithText(string testText) => new Implementation.PlainText(testText);
    }
}
