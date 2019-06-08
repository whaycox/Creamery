using System;
using System.Collections.Generic;
using System.Linq;

namespace Curds.CLI.Implementation
{
    using Domain;
    using Formatting;
    using Formatting.Text.Domain;
    using Formatting.Token.Abstraction;
    using Formatting.Token.Implementation;
    using Operations.Implementation;

    public class UsageOperation : BooleanOperation
    {
        private const string HelpSymbol = "?";
        private const string Help = nameof(Help);

        public override IEnumerable<string> Aliases => new string[] { Help, HelpSymbol };

        public override string Name => Help;
        public override string Description => "Print Usage";

        public Formatted Format(string message, string applicationDescription, IEnumerable<Operation> operations) => new Formatted()
            .Color(CLIEnvironment.Error, UsageMessage(message))
            .AppendLine(FormatDescription(applicationDescription))
            .Append(OperationUsages(operations));
        private Formatted UsageMessage(string message) => new Formatted()
            .AppendLine(new PlainText(message));
        private Formatted FormatDescription(string applicationDescription) => new Formatted()
            .Concatenate(null, " | ", null, new List<IToken> { AppName, new PlainText(applicationDescription) });
        private Formatted AppName => new Formatted()
            .Color(CLIEnvironment.Application, new PlainText(AppDomain.CurrentDomain.FriendlyName));
        private Formatted OperationUsages(IEnumerable<Operation> operations) => new Formatted()
            .ColorLine(CLIEnvironment.Operation, OperationHeader)
            .IndentLine(operations.Select(o => o.Usage));
        private IToken OperationHeader => new PlainText("Operations:");
    }
}
