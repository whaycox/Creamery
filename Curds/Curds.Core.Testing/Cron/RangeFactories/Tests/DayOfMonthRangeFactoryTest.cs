using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Curds.Cron.RangeFactories.Tests
{
    using Cron.Abstraction;
    using Implementation;
    using FieldDefinitions.Implementation;
    using Template;
    using Abstraction;

    [TestClass]
    public class DayOfMonthRangeFactoryTest : BaseRangeFactoryTemplate
    {
        private Mock<IRangeFactoryChain<DayOfMonthFieldDefinition>> MockRangeLinkFactory = new Mock<IRangeFactoryChain<DayOfMonthFieldDefinition>>();

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
