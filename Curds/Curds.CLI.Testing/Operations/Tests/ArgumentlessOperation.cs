using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Curds.Domain.Application;
using Curds.Domain.CLI.Operations;
using Curds.Domain.CLI;

namespace Curds.CLI.Operations.Tests
{
    using Formatting;

    [TestClass]
    public class ArgumentlessOperation : OperationTemplate<ArgumentlessOperation<MockApplication>>
    {
        private ArgumentlessOperation<MockApplication> _obj = null;
        protected override ArgumentlessOperation<MockApplication> TestObject => _obj;

        protected override int ExpectedUsageWrites => 61;

        [TestInitialize]
        public void BuildObj()
        {
            _obj = new MockArgumentlessOperation(Application.Dispatch.MockQueryingCommand);
        }

        protected override void VerifyUsage(MockConsoleWriter writer) =>
            writer.StartsWith(NewLine(true))
                .ThenHas(TextColor(CLIEnvironment.Operation))
                .ThenHas(NewLine(false))
                .ThenHas(AliasedOptionValue.AliasStart)
                .ThenHas($"{MockArgumentlessOperation.OperationIdentifier}{nameof(MockArgumentlessOperation)}")
                .ThenHas(AliasedOptionValue.AliasSeparator)
                .ThenHas($"{MockArgumentlessOperation.OperationIdentifier}{nameof(MockArgumentlessOperation)}{nameof(MockArgumentlessOperation.Aliases)}")
                .ThenHas(AliasedOptionValue.AliasEnd)
                .ThenHas(TextColor(CLIEnvironment.DefaultTextColor))
                .ThenHas(" ")
                .ThenHas(TextColor(CLIEnvironment.Value))
                .ThenHas(Operations.Value.SyntaxStart)
                .ThenHas(nameof(MockValue))
                .ThenHas(Operations.Value.SyntaxEnd)
                .ThenHas(" ")
                .ThenHas(Operations.Value.SyntaxStart)
                .ThenHas(nameof(MockValue))
                .ThenHas(Operations.Value.SyntaxEnd)
                .ThenHas(" ")
                .ThenHas(Operations.Value.SyntaxStart)
                .ThenHas(nameof(MockValue))
                .ThenHas(Operations.Value.SyntaxEnd)
                .ThenHas(TextColor(CLIEnvironment.DefaultTextColor))
                .ThenHas(Environment.NewLine)
                .ThenHas(NewLine(true))
                .ThenHas(TextColor(CLIEnvironment.Operation))
                .ThenHas(NewLine(false))
                .ThenHas(nameof(MockArgumentlessOperation))
                .ThenHas(TextColor(CLIEnvironment.DefaultTextColor))
                .ThenHas($": {nameof(MockArgumentlessOperation)}{nameof(MockArgumentlessOperation.Description)}")
                .ThenHas(Environment.NewLine)
                .ThenHas(NewLine(true))
                .ThenHas(TextColor(CLIEnvironment.Value))
                .ThenHas(NewLine(false))
                .ThenHas("Values:")
                .ThenHas(Environment.NewLine)
                .ThenHas(NewLine(true))
                .ThenHas(TextColor(CLIEnvironment.DefaultTextColor))
                .ThenHas(Indents(1))
                .ThenHas(TextColor(CLIEnvironment.Value))
                .ThenHas(NewLine(false))
                .ThenHas($"{CLIEnvironment.DefaultIndentation}{nameof(MockValue)}")
                .ThenHas(TextColor(CLIEnvironment.DefaultTextColor))
                .ThenHas($": {nameof(MockValue)}{nameof(MockValue.Description)}")
                .ThenHas(Environment.NewLine)
                .ThenHas(NewLine(true))
                .ThenHas(TextColor(CLIEnvironment.Value))
                .ThenHas(NewLine(false))
                .ThenHas($"{CLIEnvironment.DefaultIndentation}{nameof(MockValue)}")
                .ThenHas(TextColor(CLIEnvironment.DefaultTextColor))
                .ThenHas($": {nameof(MockValue)}{nameof(MockValue.Description)}")
                .ThenHas(Environment.NewLine)
                .ThenHas(NewLine(true))
                .ThenHas(TextColor(CLIEnvironment.Value))
                .ThenHas(NewLine(false))
                .ThenHas($"{CLIEnvironment.DefaultIndentation}{nameof(MockValue)}")
                .ThenHas(TextColor(CLIEnvironment.DefaultTextColor))
                .ThenHas($": {nameof(MockValue)}{nameof(MockValue.Description)}")
                .ThenHas(Environment.NewLine)
                .ThenHas(NewLine(true))
                .ThenHas(Indents(0));
    }
}
