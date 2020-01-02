using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace Curds.Cron.FieldFactories.Tests
{
    using Cron.Abstraction;
    using FieldDefinitions.Implementation;
    using Fields.Implementation;
    using Implementation;
    using Template;

    [TestClass]
    public class MinuteFieldFactoryTest : BaseFieldFactoryTemplate
    {
        private Mock<ICronRangeFactory<MinuteFieldDefinition>> MockRangeFactory = new Mock<ICronRangeFactory<MinuteFieldDefinition>>();

        private MinuteFieldFactory _testObject = null;
        protected override ICronFieldFactory TestObject => _testObject;

        protected override Type ExpectedParseType => typeof(MinuteField);

        protected override void VerifyStringWasParsed(string range) => MockRangeFactory
            .Verify(factory => factory.Parse(range), Times.Once);

        [TestInitialize]
        public void Init()
        {
            _testObject = new MinuteFieldFactory(MockRangeFactory.Object);
        }
    }
}
