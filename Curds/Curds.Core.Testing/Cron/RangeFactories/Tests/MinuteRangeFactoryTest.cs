using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Curds.Cron.RangeFactories.Tests
{
    using Cron.Abstraction;
    using Implementation;
    using Abstraction;
    using Template;
    using FieldDefinitions.Implementation;

    [TestClass]
    public class MinuteRangeFactoryTest : BaseRangeFactoryTemplate
    {
        private Mock<IRangeFactoryChain<MinuteFieldDefinition>> MockRangeLinkFactory = new Mock<IRangeFactoryChain<MinuteFieldDefinition>>();

        private MinuteRangeFactory _testObject = null;
        protected override ICronRangeFactory TestObject => _testObject;

        [TestInitialize]
        public void Init()
        {
            _testObject = new MinuteRangeFactory(MockRangeLinkFactory.Object);
        }

        protected override void SetupRangeLinkFactory() => MockRangeLinkFactory
            .Setup(factory => factory.BuildChain())
            .Returns(MockRangeLink.Object);

        protected override void VerifyChainWasRetrieved() => MockRangeLinkFactory
            .Verify(factory => factory.BuildChain(), Times.Once);
    }
}
