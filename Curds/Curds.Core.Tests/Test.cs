using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace Curds
{
    using Time.Mock;

    public abstract class Test
    {
        public TestContext TestContext { get; set; }

        protected ITime MockTime = new ITime();

        private string TestName => $"{TestContext.FullyQualifiedTestClassName}.{TestContext.TestName}";

        [TestInitialize]
        public void LogStart()
        {
            Debug.WriteLine($"Starting {nameof(Test)} {TestName}");
        }

        [TestCleanup]
        public void LogEnd()
        {
            Debug.WriteLine($"Ended {nameof(Test)} {TestName} with status {TestContext.CurrentTestOutcome}");
        }
    }

    public abstract class Test<T> : Test
    {
        protected abstract T TestObject { get; }
    }
}
