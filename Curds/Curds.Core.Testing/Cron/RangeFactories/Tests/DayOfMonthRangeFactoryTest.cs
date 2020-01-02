using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Curds.Cron.RangeFactories.Tests
{
    using Cron.Abstraction;
    using Implementation;
    using Chains.Abstraction;
    using Template;

    [TestClass]
    public class DayOfMonthRangeFactoryTest : BaseRangeFactoryTemplate
    {
        private Mock<IDayOfMonthRangeLinkFactory> MockRangeLinkFactory = new Mock<IDayOfMonthRangeLinkFactory>();

        private DayOfMonthRangeFactory _testObject = null;
        protected override ICronRangeFactory TestObject => _testObject;

        [TestInitialize]
        public void Init()
        {
            _testObject = new DayOfMonthRangeFactory(MockRangeLinkFactory.Object);
        }

        protected override void SetupRangeLinkFactory() => MockRangeLinkFactory
            .Setup(factory => factory.BuildChain())
            .Returns(MockRangeLink.Object);

        protected override void VerifyChainWasRetrieved() => MockRangeLinkFactory
            .Verify(factory => factory.BuildChain(), Times.Once);
    }
}
