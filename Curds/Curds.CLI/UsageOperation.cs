using System;
using System.Collections.Generic;
using System.Linq;

namespace Curds.CLI
{
    using Formatting;
    using Formatting.Tokens;
    using Operations;

    public class UsageOperation : BooleanOperation
    {
        private const string HelpSymbol = "?";
        private const string Help = nameof(Help);

        public override IEnumerable<string> Aliases => new string[] { Help, HelpSymbol };

        public override string Name => Help;
        public override string Description => "Print Usage";

        public FormattedText Format(string message, string applicationDescription, IEnumerable<Operation> operations) => FormattedText.New
            .Color(CLIEnvironment.Error, UsageMessage(message))
            .AppendLine(FormatDescription(applicationDescription))
            .Append(OperationUsages(operations));
        private FormattedText UsageMessage(string message) => FormattedText.New
            .AppendLine(PlainTextToken.Create(message));
        private FormattedText FormatDescription(string applicationDescription) => FormattedText.New
            .Concatenate(null, " | ", null, new List<BaseTextToken> { AppName, PlainTextToken.Create(applicationDescription) });
        private FormattedText AppName => FormattedText.New
            .Color(CLIEnvironment.Application, PlainTextToken.Create(AppDomain.CurrentDomain.FriendlyName));
        private FormattedText OperationUsages(IEnumerable<Operation> operations) => FormattedText.New
            .ColorLine(CLIEnvironment.Operation, OperationHeader)
            .IndentLine(operations.Select(o => o.Usage));
        private BaseTextToken OperationHeader => PlainTextToken.Create("Operations:");
    }
}
