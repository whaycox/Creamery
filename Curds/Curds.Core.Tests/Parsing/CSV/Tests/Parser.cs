using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Parsing.CSV.Tests
{
    using Abstraction;

    [TestClass]
    public class Parser : Template.ICSVParser<Implementation.Parser>
    {
        private static ICSVOptions DefaultOptions => new Domain.CSVOptions();

        protected override Implementation.Parser TestObject { get; } = new Implementation.Parser(DefaultOptions);

        protected override Implementation.Parser BuildWithOptions(ICSVOptions options) => new Implementation.Parser(options);

    }
}
