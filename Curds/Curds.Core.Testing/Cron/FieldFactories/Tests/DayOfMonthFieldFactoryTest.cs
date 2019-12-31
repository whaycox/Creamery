using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace Curds.Cron.FieldFactories.Tests
{
    using Cron.Abstraction;
    using Fields.Implementation;
    using Implementation;
    using RangeFactories.Abstraction;
    using Template;

    [TestClass]
    public class DayOfMonthFieldFactoryTest : BaseFieldFactoryTemplate
    {
        private Mock<IDayOfMonthRangeFactory> MockRangeFactory = new Mock<IDayOfMonthRangeFactory>();

        private DayOfMonthFieldFactory _testObject = null;
        protected override ICronFieldFactory TestObject => _testObject;

        protected override Type ExpectedParseType => typeof(DayOfMonthField);

        protected override void VerifyStringWasParsed(string range) => MockRangeFactory
            .Verify(factory => factory.Parse(range), Times.Once);

        [TestInitialize]
        public void Init()
        {
            _testObject = new DayOfMonthFieldFactory(MockRangeFactory.Object);
        }
    }
}
