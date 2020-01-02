using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Curds.Cron.RangeFactories.Links.Template
{
    using Cron.Abstraction;
    using Cron.Template;
    using RangeFactories.Abstraction;

    public abstract class BaseRangeLinkTemplate : FieldDefinitionTemplate
    {
        protected Mock<IRangeFactoryLink> MockRangeLink = new Mock<IRangeFactoryLink>();

        protected abstract IRangeFactoryLink InterfaceTestObject { get; }

        [TestMethod]
        public void SuccessorIsSuppliedLink()
        {
            Assert.AreSame(MockRangeLink.Object, InterfaceTestObject.Successor);
        }
    }
}
