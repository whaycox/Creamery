﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Curds.Cron.RangeFactories.Tests
{
    using Cron.Abstraction;
    using Implementation;
    using FieldDefinitions.Implementation;
    using Abstraction;
    using Template;

    [TestClass]
    public class MonthRangeFactoryTest : BaseRangeFactoryTemplate
    {
        private Mock<IRangeFactoryChain<MonthFieldDefinition>> MockRangeLinkFactory = new Mock<IRangeFactoryChain<MonthFieldDefinition>>();

        private MonthRangeFactory _testObject = null;
        protected override ICronRangeFactory TestObject => _testObject;

        [TestInitialize]
        public void Init()
        {
            _testObject = new MonthRangeFactory(MockRangeLinkFactory.Object);
        }

        protected override void SetupRangeLinkFactory() => MockRangeLinkFactory
            .Setup(factory => factory.BuildChain())
            .Returns(MockRangeLink.Object);

        protected override void VerifyChainWasRetrieved() => MockRangeLinkFactory
            .Verify(factory => factory.BuildChain(), Times.Once);
    }
}
