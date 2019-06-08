using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Text.Tests
{
    using Template;

    [TestClass]
    public class IndentedWriter : IIndentedWriter<Implementation.IndentedWriter>
    {
        protected override Implementation.IndentedWriter TestObject { get; } = new Implementation.IndentedWriter();
    }
}
