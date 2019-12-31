using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Cron.RangeLinkFactories.Template
{
    using Cron.Abstraction;
    using Implementation;
    using RangeLinks.Implementation;

    public abstract class BaseRangeLinkFactoryTemplate
    {
        protected ICronRangeLink TestRangeLink = null;

        protected abstract ICronRangeLinkFactory TestObject { get; }

        protected void SetTestRangeLinkToStartOfChain() => TestRangeLink = TestObject.StartOfChain;

        protected void VerifyAndIncrementTestRangeLink(Type expectedType)
        {
            Assert.IsInstanceOfType(TestRangeLink, expectedType);
            TestRangeLink = TestRangeLink.Successor;
        }

        protected void VerifyDefaultChain<TFieldDefinition>()
            where TFieldDefinition : ICronFieldDefinition, new()
        {
            VerifyAndIncrementTestRangeLink(typeof(SingleValueRangeLink<TFieldDefinition>));
            VerifyAndIncrementTestRangeLink(typeof(RangeValueRangeLink<TFieldDefinition>));
            VerifyAndIncrementTestRangeLink(typeof(WildcardRangeLink));
        }
    }
}
