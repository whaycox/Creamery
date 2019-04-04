using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Curds.Domain.Application;
using Curds.Domain.CLI;
using Curds.Domain.CLI.Operations;

namespace Curds.CLI.Operations.Tests
{
    using Formatting;

    [TestClass]
    public class Operation : OperationTemplate<Operation<MockApplication>>
    {
        private Operation<MockApplication> _obj = null;
        protected override Operation<MockApplication> TestObject => _obj;

        protected override int ExpectedUsageWrites => 106;

        [TestInitialize]
        public void BuildObj()
        {
            _obj = new MockOperation(Application.Dispatch.MockCommand);
        }

        protected override void VerifyUsage(MockConsoleWriter writer) =>
            writer.StartsWith(NewLine(true))
                .ThenHas(TextColor(CLIEnvironment.Operation))
                .ThenHas(NewLine(false))
                .ThenHas(AliasedOptionValue.AliasStart)
                .ThenHas($"{MockOperation.OperationIdentifier}{nameof(MockOperation)}")
                .ThenHas(AliasedOptionValue.AliasSeparator)
                .ThenHas($"{MockOperation.OperationIdentifier}{nameof(MockOperation)}{nameof(MockOperation.Aliases)}")
                .ThenHas(AliasedOptionValue.AliasEnd)
                .ThenHas(TextColor(CLIEnvironment.DefaultTextColor))
                .ThenHas(" ")
                .ThenHas(TextColor(CLIEnvironment.Operation))
                .ThenHas(nameof(MockOperation))
                .ThenHas(TextColor(CLIEnvironment.DefaultTextColor))
                .ThenHas($": {nameof(MockOperation)}{nameof(MockOperation.Description)}")
                .ThenHas(Environment.NewLine)
                .ThenHas(NewLine(true))
                .ThenHas(TextColor(CLIEnvironment.Argument))
                .ThenHas(NewLine(false))
                .ThenHas("Arguments:")
                .ThenHas(Environment.NewLine)
                .ThenHas(NewLine(true))
                .ThenHas(TextColor(CLIEnvironment.DefaultTextColor))
                .ThenHas(Indents(1))
                .ThenHas(TextColor(CLIEnvironment.Argument))
                .ThenHas(NewLine(false))
                .ThenHas($"\t{AliasedOptionValue.AliasStart}")
                .ThenHas($"{Operations.Argument.ArgumentIdentifier}1{nameof(MockArgument)}")
                .ThenHas(AliasedOptionValue.AliasSeparator)
                .ThenHas($"{Operations.Argument.ArgumentIdentifier}1{nameof(MockArgument.Aliases)}")
                .ThenHas(AliasedOptionValue.AliasEnd)
                .ThenHas(TextColor(CLIEnvironment.DefaultTextColor))
                .ThenHas(" ")
                .ThenHas(TextColor(CLIEnvironment.Value))
                .ThenHas(Operations.Value.SyntaxStart)
                .ThenHas(nameof(MockValue))
                .ThenHas(Operations.Value.SyntaxEnd)
                .ThenHas(TextColor(CLIEnvironment.DefaultTextColor))
                .ThenHas(Environment.NewLine)
                .ThenHas(NewLine(true))
                .ThenHas(TextColor(CLIEnvironment.Argument))
                .ThenHas(NewLine(false))
                .ThenHas($"\t1{nameof(MockArgument)}")
                .ThenHas(TextColor(CLIEnvironment.DefaultTextColor))
                .ThenHas($": {nameof(MockArgument)}{nameof(MockArgument.Description)}")
                .ThenHas(Environment.NewLine)
                .ThenHas(NewLine(true))
                .ThenHas(TextColor(CLIEnvironment.Value))
                .ThenHas(NewLine(false))
                .ThenHas($"\tValues:")
                .ThenHas(Environment.NewLine)
                .ThenHas(NewLine(true))
                .ThenHas(TextColor(CLIEnvironment.DefaultTextColor))
                .ThenHas(Indents(2))
                .ThenHas(TextColor(CLIEnvironment.Value))
                .ThenHas(NewLine(false))
                .ThenHas($"\t\t{nameof(MockValue)}")
                .ThenHas(TextColor(CLIEnvironment.DefaultTextColor))
                .ThenHas($": {nameof(MockValue)}{nameof(MockValue.Description)}")
                .ThenHas(Environment.NewLine)
                .ThenHas(NewLine(true))
                .ThenHas(Indents(1))
                .ThenHas(NewLine(false))
                .ThenHas($"\t{Environment.NewLine}")
                .ThenHas(NewLine(true))
                .ThenHas(TextColor(CLIEnvironment.Argument))
                .ThenHas(NewLine(false))
                .ThenHas($"\t{AliasedOptionValue.AliasStart}")
                .ThenHas($"{Operations.Argument.ArgumentIdentifier}2{nameof(MockArgument)}")
                .ThenHas(AliasedOptionValue.AliasSeparator)
                .ThenHas($"{Operations.Argument.ArgumentIdentifier}2{nameof(MockArgument.Aliases)}")
                .ThenHas(AliasedOptionValue.AliasEnd)
                .ThenHas(TextColor(CLIEnvironment.DefaultTextColor))
                .ThenHas(" ")
                .ThenHas(TextColor(CLIEnvironment.Value))
                .ThenHas(Operations.Value.SyntaxStart)
                .ThenHas(nameof(MockValue))
                .ThenHas(Operations.Value.SyntaxEnd)
                .ThenHas(TextColor(CLIEnvironment.DefaultTextColor))
                .ThenHas(Environment.NewLine)
                .ThenHas(NewLine(true))
                .ThenHas(TextColor(CLIEnvironment.Argument))
                .ThenHas(NewLine(false))
                .ThenHas($"\t2{nameof(MockArgument)}")
                .ThenHas(TextColor(CLIEnvironment.DefaultTextColor))
                .ThenHas($": {nameof(MockArgument)}{nameof(MockArgument.Description)}")
                .ThenHas(Environment.NewLine)
                .ThenHas(NewLine(true))
                .ThenHas(TextColor(CLIEnvironment.Value))
                .ThenHas(NewLine(false))
                .ThenHas($"\tValues:")
                .ThenHas(Environment.NewLine)
                .ThenHas(NewLine(true))
                .ThenHas(TextColor(CLIEnvironment.DefaultTextColor))
                .ThenHas(Indents(2))
                .ThenHas(TextColor(CLIEnvironment.Value))
                .ThenHas(NewLine(false))
                .ThenHas($"\t\t{nameof(MockValue)}")
                .ThenHas(TextColor(CLIEnvironment.DefaultTextColor))
                .ThenHas($": {nameof(MockValue)}{nameof(MockValue.Description)}")
                .ThenHas(Environment.NewLine)
                .ThenHas(NewLine(true))
                .ThenHas(Indents(1))
                .ThenHas(NewLine(false))
                .ThenHas($"\t{Environment.NewLine}")
                .ThenHas(NewLine(true))
                .ThenHas(Indents(0));
    }
}
