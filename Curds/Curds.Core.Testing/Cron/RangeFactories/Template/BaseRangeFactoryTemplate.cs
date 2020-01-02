using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace Curds.Cron.RangeFactories.Template
{
    using Cron.Abstraction;
    using Abstraction;

    public abstract class BaseRangeFactoryTemplate
    {
        protected string TestRange = nameof(TestRange);

        protected Mock<IRangeFactoryLink> MockRangeLink = new Mock<IRangeFactoryLink>();
        protected Mock<ICronRange> MockRange = new Mock<ICronRange>();

        protected abstract ICronRangeFactory TestObject { get; }

        protected void SetupChainOfNLinks(int numberOfLinks)
        {
            var parseSequence = MockRangeLink.SetupSequence(link => link.HandleParse(It.IsAny<string>()));
            var successorSequence = MockRangeLink.SetupSequence(link => link.Successor);
            for (int i = 0; i < numberOfLinks; i++)
            {
                if (i == numberOfLinks - 1)
                    parseSequence.Returns(MockRange.Object);
                else
                {
                    parseSequence.Returns(value: null);
                    successorSequence.Returns(MockRangeLink.Object);
                }
            }
        }

        [TestInitialize]
        public void SetupBaseRangeFactoryTemplate()
        {
            SetupRangeLinkFactory();
        }
        protected abstract void SetupRangeLinkFactory();

        [TestMethod]
        public void RetrievesChainFromFactory()
        {
            SetupChainOfNLinks(1);

            TestObject.Parse(TestRange);

            VerifyChainWasRetrieved();
        }
        protected abstract void VerifyChainWasRetrieved();

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(7)]
        [DataRow(10)]
        [DataRow(15)]
        public void ParsesLinkUntilReturn(int numberOfLinks)
        {
            SetupChainOfNLinks(numberOfLinks);

            TestObject.Parse(TestRange);

            MockRangeLink.Verify(link => link.HandleParse(TestRange), Times.Exactly(numberOfLinks));
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(7)]
        [DataRow(10)]
        [DataRow(15)]
        public void RetrievesSuccessorIfChainContinues(int numberOfLinks)
        {
            SetupChainOfNLinks(numberOfLinks);

            TestObject.Parse(TestRange);

            MockRangeLink.Verify(link => link.Successor, Times.Exactly(numberOfLinks - 1));
        }

        [TestMethod]
        public void ReturnsRangeLinkParseResult()
        {
            SetupChainOfNLinks(1);

            ICronRange actual = TestObject.Parse(TestRange);

            Assert.AreSame(MockRange.Object, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void NoSuccessorThrows()
        {
            TestObject.Parse(TestRange);
        }
    }
}
