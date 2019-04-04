using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain.Application;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Curds.Domain.CLI;

namespace Curds.CLI.Operations
{
    using Formatting;

    public abstract class OperationTemplate<T> : FormattingTemplate<T> where T : Operation<MockApplication>
    {
        private MockOptions Options => new MockOptions();
        protected MockApplication Application = null;

        [TestInitialize]
        public void Init()
        {
            Application = new MockApplication(Options);
        }

        [TestMethod]
        public void UsageIsProperlyFormatted()
        {
            TestObject.Usage.Write(Writer);
            Assert.AreEqual(ExpectedUsageWrites, Writer.Writes.Count);
            VerifyUsage(Writer);
        }
        protected abstract int ExpectedUsageWrites { get; }
        protected abstract void VerifyUsage(MockConsoleWriter writer);
    }
}
