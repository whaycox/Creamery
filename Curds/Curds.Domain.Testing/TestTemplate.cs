using System;
using System.Collections.Generic;
using System.Text;
using Curds.Infrastructure.Cron;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace Curds.Domain
{
    using DateTimes;

    public abstract class TestTemplate
    {
        public TestContext TestContext { get; set; }

        protected MockDateTime Time = new MockDateTime();

        private string TestName => $"{TestContext.FullyQualifiedTestClassName}.{TestContext.TestName}";

        [TestInitialize]
        public void LogStart()
        {
            Debug.WriteLine($"Starting Test {TestName}");
        }

        [TestCleanup]
        public void LogEnd()
        {
            Debug.WriteLine($"Ended Test {TestName} with status {TestContext.CurrentTestOutcome}");
        }
    }

    public abstract class TestTemplate<T> : TestTemplate
    {
        protected abstract T TestObject { get; }
    }
}
