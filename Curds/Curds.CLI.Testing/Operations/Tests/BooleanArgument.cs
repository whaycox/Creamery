using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain.CLI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Curds.Domain.CLI.Operations;

namespace Curds.CLI.Operations.Tests
{
    using Formatting;

    [TestClass]
    public class BooleanArgument : OptionValueTemplate<Operations.BooleanArgument>
    {
        protected override int SyntaxExpectedWrites => 9;

        protected override int UsageExpectedWrites => 18;

        protected override Operations.BooleanArgument TestObject { get; } = new MockBooleanArgument();

        protected override void VerifySyntax(MockConsoleWriter writer) =>
            writer.StartsWith(NewLine(true))
                .ThenHas(TextColor(CLIEnvironment.Argument))
                .ThenHas(NewLine(false))
                .ThenHas(AliasedOptionValue.AliasStart)
                .ThenHas($"{Operations.Argument.ArgumentIdentifier}{nameof(MockBooleanArgument)}")
                .ThenHas(AliasedOptionValue.AliasSeparator)
                .ThenHas($"{Operations.Argument.ArgumentIdentifier}{nameof(MockBooleanArgument)}{nameof(MockBooleanArgument.Aliases)}")
                .ThenHas(AliasedOptionValue.AliasEnd)
                .ThenHas(TextColor(CLIEnvironment.DefaultTextColor));

        protected override void VerifyUsage(MockConsoleWriter writer) =>
            writer.StartsWith(NewLine(true))
                .ThenHas(TextColor(CLIEnvironment.Argument))
                .ThenHas(NewLine(false))
                .ThenHas(AliasedOptionValue.AliasStart)
                .ThenHas($"{Operations.Argument.ArgumentIdentifier}{nameof(MockBooleanArgument)}")
                .ThenHas(AliasedOptionValue.AliasSeparator)
                .ThenHas($"{Operations.Argument.ArgumentIdentifier}{nameof(MockBooleanArgument)}{nameof(MockBooleanArgument.Aliases)}")
                .ThenHas(AliasedOptionValue.AliasEnd)
                .ThenHas(TextColor(CLIEnvironment.DefaultTextColor))
                .ThenHas(Environment.NewLine)
                .ThenHas(NewLine(true))
                .ThenHas(TextColor(CLIEnvironment.Argument))
                .ThenHas(NewLine(false))
                .ThenHas(nameof(MockBooleanArgument))
                .ThenHas(TextColor(CLIEnvironment.DefaultTextColor))
                .ThenHas($": {nameof(MockBooleanArgument)}{nameof(MockBooleanArgument.Description)}")
                .ThenHas(Environment.NewLine)
                .ThenHas(NewLine(true));
    }
}
