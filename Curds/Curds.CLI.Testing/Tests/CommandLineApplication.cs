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
        public void NullArgumentsProvidesStandardUsage()
        {
            TestObject.Execute(null);
            Writer.StartsWith(NewLine(true))
                .ThenHas(TextColor(CLIEnvironment.Error))
                .ThenHas(NewLine(false))
                .ThenHas("Please provide arguments")
                .ThenHas(Environment.NewLine)
                .ThenHas(NewLine(true))
                .ThenHas(TextColor(CLIEnvironment.DefaultTextColor))
                .ThenHas(TextColor(CLIEnvironment.Application))
                .ThenHas(NewLine(false))
                .ThenHas(Testing.ApplicationName)
                .ThenHas(TextColor(CLIEnvironment.DefaultTextColor))
                .ThenHas(" | ")
                .ThenHas(nameof(MockApplication))
                .ThenHas(Environment.NewLine)
                .ThenHas(NewLine(true))
                .ThenHas(TextColor(CLIEnvironment.Operation))
                .ThenHas(NewLine(false))
                .ThenHas("Operations:")
                .ThenHas(Environment.NewLine)
                .ThenHas(NewLine(true))
                .ThenHas(TextColor(CLIEnvironment.DefaultTextColor))
                .ThenHas(Indents(1));
        }
    }

}
