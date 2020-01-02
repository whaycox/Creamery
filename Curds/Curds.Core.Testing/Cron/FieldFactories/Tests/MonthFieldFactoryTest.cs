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
    public class MonthFieldFactoryTest : BaseFieldFactoryTemplate
    {
        private Mock<ICronRangeFactory<MonthFieldDefinition>> MockRangeFactory = new Mock<ICronRangeFactory<MonthFieldDefinition>>();

        private MonthFieldFactory _testObject = null;
        protected override ICronFieldFactory TestObject => _testObject;

        protected override Type ExpectedParseType => typeof(MonthField);

        protected override void VerifyStringWasParsed(string range) => MockRangeFactory
            .Verify(factory => factory.Parse(range), Times.Once);

        [TestInitialize]
        public void Init()
        {
            _testObject = new MonthFieldFactory(MockRangeFactory.Object);
        }
    }
}
