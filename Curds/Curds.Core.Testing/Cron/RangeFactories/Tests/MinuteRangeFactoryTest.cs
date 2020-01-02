using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Curds.Cron.RangeFactories.Tests
{
    using Cron.Abstraction;
    using Implementation;
    using Chains.Abstraction;
    using Template;

    [TestClass]
    public class MinuteRangeFactoryTest : BaseRangeFactoryTemplate
    {
        private Mock<IMinuteRangeLinkFactory> MockRangeLinkFactory = new Mock<IMinuteRangeLinkFactory>();

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
