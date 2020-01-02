using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Cron.RangeFactories.Chains.Template
{
    using Cron.Abstraction;
    using Implementation;
    using Links.Implementation;
    using RangeFactories.Abstraction;

    public abstract class BaseChainTemplate
    {
        protected IRangeFactoryLink TestRangeLink = null;

        protected abstract IRangeFactoryChain TestObject { get; }

        protected void SetTestRangeLinkToStartOfChain() => TestRangeLink = TestObject.BuildChain();

        protected void VerifyAndIncrementTestRangeLink(Type expectedType)
        {
            Assert.IsInstanceOfType(TestRangeLink, expectedType);
            TestRangeLink = TestRangeLink.Successor;
        }

        protected void VerifyDefaultChain<TFieldDefinition>()
            where TFieldDefinition : ICronFieldDefinition, new()
        {
            VerifyAndIncrementTestRangeLink(typeof(SingleValueLink<TFieldDefinition>));
            VerifyAndIncrementTestRangeLink(typeof(RangeValueLink<TFieldDefinition>));
            VerifyAndIncrementTestRangeLink(typeof(WildcardLink<TFieldDefinition>));
        }
    }
}
