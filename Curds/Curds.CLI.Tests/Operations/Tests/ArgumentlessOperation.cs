using System;
using System.Collections.Generic;
using System.Text;
using Curds.CLI.Operations.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.CLI.Operations.Tests
{
    using Mock;

    [TestClass]
    public class ArgumentlessOperation : Template.Console<Implementation.ArgumentlessOperation>
    {
        private Mock.ArgumentlessOperation _obj = new Mock.ArgumentlessOperation();
        protected override Implementation.ArgumentlessOperation TestObject => _obj;

        [TestMethod]
        public void SyntaxIsAliasesWithValues()
        {
            TestObject.Syntax.Write(MockConsole);
            MockConsole
                .VerifyOperations(21)
                .VerifyArgumentlessOperationSyntax()
                .IsFinished();
        }

        [TestMethod]
        public void UsageIsSyntaxWithValueUsage()
        {
            TestObject.Usage.Write(MockConsole);
            MockConsole
                .VerifyOperations(52)
                .VerifyArgumentlessOperationUsage()
                .IsFinished();
        }

        [TestMethod]
        public void ParsesValuesIntoConstantKey()
        {
            MockArgumentCrawler.LoadScript(ArgumentlessOperationScript);
            var parsed = TestObject.Parse(MockArgumentCrawler);
            Assert.AreEqual(1, parsed.Count);
            var constant = parsed[Implementation.ArgumentlessOperation.ArgumentlessKey];
            Assert.AreEqual(3, constant.Count);
            Assert.AreEqual(One, constant[0].RawValue);
            Assert.AreEqual(Two, constant[1].RawValue);
            Assert.AreEqual(Three, constant[2].RawValue);
        }
        private string[] ArgumentlessOperationScript => new string[]
        {
            One,
            Two,
            Three,
        };

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        public void ParseThrowsIfNotAllValuesSupplied(int valuesToSupply)
        {
            MockArgumentCrawler.LoadScript(ValueScript(valuesToSupply));
            Assert.ThrowsException<InvalidOperationException>(() => TestObject.Parse(MockArgumentCrawler));
        }

    }
}
