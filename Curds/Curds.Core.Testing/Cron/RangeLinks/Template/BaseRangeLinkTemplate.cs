using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Curds.Cron.RangeLinks.Template
{
    using Cron.Abstraction;
    using Cron.Template;

    public abstract class BaseRangeLinkTemplate : FieldDefinitionTemplate
    {
        protected Mock<ICronRangeLink> MockRangeLink = new Mock<ICronRangeLink>();

        protected abstract ICronRangeLink InterfaceTestObject { get; }

        [TestMethod]
        public void SuccessorIsSuppliedLink()
        {
            Assert.AreSame(MockRangeLink.Object, InterfaceTestObject.Successor);
        }
    }
}
