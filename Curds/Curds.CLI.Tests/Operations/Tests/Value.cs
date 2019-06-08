using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.CLI.Operations.Tests
{
    using Enumerations;
    using CLI.Domain;
    using Mock;

    [TestClass]
    public class Value : Template.Console<Mock.Value>
    {
        protected override Mock.Value TestObject { get; } = new Mock.Value();

        [TestMethod]
        public void SyntaxIsEnclosedName()
        {
            TestObject.Syntax.Write(MockConsole);
            MockConsole
                .VerifyOperations(3)
                .VerifyValueSyntax()
                .IsFinished();
        }

        [TestMethod]
        public void UsageIsNameAndDescription()
        {
            TestObject.Usage.Write(MockConsole);
            MockConsole
                .VerifyOperations(4)
                .VerifyValueUsage()
                .IsFinished();
        }

        [TestMethod]
        public void RawValueIsNullAtStart()
        {
            Assert.IsNull(TestObject.RawValue);
        }

        [TestMethod]
        public void ParsePopulatesRawValue()
        {
            TestObject.Parse(MockArgumentCrawler);
            Assert.AreEqual(nameof(MockArgumentCrawler.Consume), TestObject.RawValue);
        }

        [TestMethod]
        public void ParseReturnsPopulatedValue()
        {
            Assert.AreEqual(nameof(MockArgumentCrawler.Consume), TestObject.Parse(MockArgumentCrawler).RawValue);
        }
    }
}
