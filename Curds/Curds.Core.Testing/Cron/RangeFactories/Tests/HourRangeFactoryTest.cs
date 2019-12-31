using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Curds.Cron.RangeFactories.Tests
{
    using Cron.Abstraction;
    using Implementation;
    using RangeLinkFactories.Abstraction;
    using Template;

    [TestClass]
    public class HourRangeFactoryTest : BaseRangeFactoryTemplate
    {
        private Mock<IHourRangeLinkFactory> MockRangeLinkFactory = new Mock<IHourRangeLinkFactory>();

        private HourRangeFactory _testObject = null;
        protected override ICronRangeFactory TestObject => _testObject;

        [TestInitialize]
        public void Init()
        {
            _testObject = new HourRangeFactory(MockRangeLinkFactory.Object);
        }

        protected override void SetupRangeLinkFactory() => MockRangeLinkFactory
            .Setup(factory => factory.StartOfChain)
            .Returns(MockRangeLink.Object);

        protected override void VerifyChainWasRetrieved() => MockRangeLinkFactory
            .Verify(factory => factory.StartOfChain, Times.Once);
    }
}
