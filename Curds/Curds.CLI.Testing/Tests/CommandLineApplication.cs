using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Curds.Domain.CLI.Formatting;
using Curds.Domain.Application;
using Curds.Domain.CLI;

namespace Curds.CLI.Tests
{
    using Formatting;

    [TestClass]
    public class CommandLineApplication : FormattingTemplate<CommandLineApplication<MockApplication>>
    {
        private readonly MockOptions Options = new MockOptions();
        private MockApplication Application = null;
        private CommandLineApplication<MockApplication> _obj = null;
        protected override CommandLineApplication<MockApplication> TestObject => _obj;

        [TestInitialize]
        public void Init()
        {
            Application = new MockApplication(Options);
            _obj = new MockCommandLineApplication(Application, Writer);
        }

        [TestMethod]
        public void UsageIsExpected()
        {
            TestObject.Execute(null);
            Assert.AreEqual(1, Writer.Writes.Count);
        }
    }

}
