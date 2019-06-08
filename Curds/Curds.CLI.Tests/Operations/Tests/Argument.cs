using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Curds.CLI.Operations.Tests
{
    using Mock;

    [TestClass]
    public class Argument : Template.Console<Implementation.Argument>
    {
        private Mock.Argument _obj = new Mock.Argument();
        protected override Implementation.Argument TestObject => _obj;

        [DataTestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void SyntaxIsAliasesFollowedByValues(bool isRequired)
        {
            _obj.TestRequired = isRequired;
            TestObject.Syntax.Write(MockConsole);
            MockConsole
                .VerifyOperations(SyntaxExpectedOperations(isRequired))
                .VerifyArgumentSyntax(isRequired)
                .IsFinished();
        }
        private int SyntaxExpectedOperations(bool isRequired) => isRequired ? 21 : 23;

        [DataTestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void UsageIsSyntaxWithValueUsage(bool isRequired)
        {
            _obj.TestRequired = isRequired;
            TestObject.Usage.Write(MockConsole);
            MockConsole
                .VerifyOperations(UsageExpectedOperations(isRequired))
                .VerifyArgumentUsage(isRequired)
                .IsFinished();
        }
        private int UsageExpectedOperations(bool isRequired) => isRequired ? 52 : 54;

        [TestMethod]
        public void ValuesHaveNoRawValueBeforeParse() => ValuesHaveNoRawValue();
        private void ValuesHaveNoRawValue()
        {
            foreach (Implementation.Value value in TestObject.Values)
                Assert.IsNull(value.RawValue);
        }

        [TestMethod]
        public void ValuesHaveNoRawValuesAfterParse()
        {
            TestObject.Parse(MockArgumentCrawler);
            ValuesHaveNoRawValue();
        }

        [TestMethod]
        public void ParseReturnsPopulatedValues()
        {
            foreach (Implementation.Value value in TestObject.Parse(MockArgumentCrawler))
                Assert.AreEqual(nameof(MockArgumentCrawler.Consume), value.RawValue);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        public void ThrowsIfNotEnoughValuesParsed(int valuesToParse)
        {
            MockArgumentCrawler.LoadScript(ValueScript(valuesToParse));
            Assert.ThrowsException<InvalidOperationException>(() => TestObject.Parse(MockArgumentCrawler));
        }
    }
}
