using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Parsing.CSV.Tests
{
    [TestClass]
    public class Parser : Template.ICSVParser<Implementation.Parser>
    {
        private static Abstraction.ICSVOptions DefaultOptions => new Domain.CSVOptions();

        protected override Implementation.Parser TestObject { get; } = new Implementation.Parser(DefaultOptions);

    }
}
