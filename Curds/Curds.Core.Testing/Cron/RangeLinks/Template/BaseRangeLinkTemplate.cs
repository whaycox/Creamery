using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Curds.Cron.RangeLinks.Template
{
    using Cron.Abstraction;

    public abstract class BaseRangeLinkTemplate
    {
        protected int TestAbsoluteMin = 3;
        protected int TestAbsoluteMax = 5;

        protected Mock<ICronFieldDefinition> MockFieldDefinition = new Mock<ICronFieldDefinition>();
        protected Mock<ICronRangeLink> MockRangeLink = new Mock<ICronRangeLink>();

        protected abstract ICronRangeLink InterfaceTestObject { get; }

        [TestInitialize]
        public void SetupBaseRangeLinkTemplate()
        {
            MockFieldDefinition
                .Setup(field => field.AbsoluteMin)
                .Returns(TestAbsoluteMin);
            MockFieldDefinition
                .Setup(field => field.AbsoluteMax)
                .Returns(TestAbsoluteMax);
        }

        [TestMethod]
        public void SuccessorIsSuppliedLink()
        {
            Assert.AreSame(MockRangeLink.Object, InterfaceTestObject.Successor);
        }
    }
}
