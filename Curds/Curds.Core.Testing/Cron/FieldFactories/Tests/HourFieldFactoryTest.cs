using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace Curds.Cron.FieldFactories.Tests
{
    using Cron.Abstraction;
    using Fields.Implementation;
    using Implementation;
    using RangeFactories.Abstraction;
    using FieldDefinitions.Implementation;
    using Template;

    [TestClass]
    public class HourFieldFactoryTest : BaseFieldFactoryTemplate
    {
        private Mock<ICronRangeFactory<HourFieldDefinition>> MockRangeFactory = new Mock<ICronRangeFactory<HourFieldDefinition>>();

        private HourFieldFactory _testObject = null;
        protected override ICronFieldFactory TestObject => _testObject;

        protected override Type ExpectedParseType => typeof(HourField);

        protected override void VerifyStringWasParsed(string range) => MockRangeFactory
            .Verify(factory => factory.Parse(range), Times.Once);

        [TestInitialize]
        public void Init()
        {
            _testObject = new HourFieldFactory(MockRangeFactory.Object);
        }
    }
}
