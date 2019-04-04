using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Curds.Domain.CLI;

namespace Curds.CLI.Operations
{
    using Formatting;

    public abstract class OptionValueTemplate<T> : FormattingTemplate<T> where T : OptionValue
    {
        [TestMethod]
        public void SyntaxHasCorrectFormatting()
        {
            TestObject.Syntax.Write(Writer);
            Assert.AreEqual(SyntaxExpectedWrites, Writer.Writes.Count);
            VerifySyntax(Writer);
        }
        protected abstract int SyntaxExpectedWrites { get; }
        protected abstract void VerifySyntax(MockConsoleWriter writer);

        [TestMethod]
        public void UsageHasCorrectFormatting()
        {
            TestObject.Usage.Write(Writer);
            Assert.AreEqual(UsageExpectedWrites, Writer.Writes.Count);
            VerifyUsage(Writer);
        }
        protected abstract int UsageExpectedWrites { get; }
        protected abstract void VerifyUsage(MockConsoleWriter writer);
    }
}
