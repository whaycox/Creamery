using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Curds.Cron.FieldFactories.Template
{
    using Cron.Abstraction;

    public abstract class BaseFieldFactoryTemplate
    {
        protected string TestFirstRange = nameof(TestFirstRange);
        protected string TestSecondRange = nameof(TestSecondRange);
        protected string TestField = null;

        protected abstract ICronFieldFactory TestObject { get; }

        [TestInitialize]
        public void SetupBaseFieldFactoryTemplate()
        {
            TestField = $"{TestFirstRange},{TestSecondRange}";
        }

        [TestMethod]
        public void ParseSplitsFieldIntoRangesForParsing()
        {
            TestObject.Parse(TestField);

            VerifyStringWasParsed(TestFirstRange);
            VerifyStringWasParsed(TestSecondRange);
        }
        protected abstract void VerifyStringWasParsed(string range);

        [TestMethod]
        public void ParseReturnsExpectedType()
        {
            ICronField actual = TestObject.Parse(TestField);

            Assert.IsInstanceOfType(actual, ExpectedParseType);
        }
        protected abstract Type ExpectedParseType { get; }
    }
}
