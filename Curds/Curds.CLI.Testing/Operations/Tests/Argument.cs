using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Curds.Domain.CLI.Operations;

namespace Curds.CLI.Operations.Tests
{
    using Curds.Domain.CLI;
    using Formatting;

    [TestClass]
    public class Argument : OptionValueTemplate<Operations.Argument>
    {
        protected override Operations.Argument TestObject { get; } = new MockArgument(1, false);

        protected override int SyntaxExpectedWrites => 15;

        protected override int UsageExpectedWrites => 39;

        protected override void VerifySyntax(MockConsoleWriter writer) =>
            writer.StartsWith(NewLine(true))
                .ThenHas(TextColor(CLIEnvironment.Argument))
                .ThenHas(NewLine(false))
                .ThenHas(AliasedOptionValue.AliasStart)
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
                .ThenHas(TextColor(CLIEnvironment.DefaultTextColor));

        protected override void VerifyUsage(MockConsoleWriter writer) =>
            writer.StartsWith(NewLine(true))
                .ThenHas(TextColor(CLIEnvironment.Argument))
                .ThenHas(NewLine(false))
                .ThenHas(AliasedOptionValue.AliasStart)
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
                .ThenHas($"1{nameof(MockArgument)}")
                .ThenHas(TextColor(CLIEnvironment.DefaultTextColor))
                .ThenHas($": {nameof(MockArgument)}{nameof(MockArgument.Description)}")
                .ThenHas(Environment.NewLine)
                .ThenHas(NewLine(true))
                .ThenHas(TextColor(CLIEnvironment.Value))
                .ThenHas(NewLine(false))
                .ThenHas($"Values:")
                .ThenHas(Environment.NewLine)
                .ThenHas(NewLine(true))                
                .ThenHas(TextColor(CLIEnvironment.DefaultTextColor))
                .ThenHas(Indents(1))
                .ThenHas(TextColor(CLIEnvironment.Value))
                .ThenHas(NewLine(false))
                .ThenHas($"\t{nameof(MockValue)}")
                .ThenHas(TextColor(CLIEnvironment.DefaultTextColor))
                .ThenHas($": {nameof(MockValue)}{nameof(MockValue.Description)}")
                .ThenHas(Environment.NewLine)
                .ThenHas(NewLine(true))
                .ThenHas(Indents(0));
    }
}
