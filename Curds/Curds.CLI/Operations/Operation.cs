using Curds.Application;
using Curds.Application.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Curds.CLI.Operations
{
    using Formatting;
    using Formatting.Tokens;

    public abstract class Operation : AliasedOptionValue
    {
        public const string OperationIdentifier = "-";
        public static string PrependIdentifier(string value) => $"{OperationIdentifier}{value}";
        public override string AliasPrefix => OperationIdentifier;

        protected abstract IEnumerable<Argument> Arguments { get; }

        protected override FormattedText ComposeAliases() => base.ComposeAliases()
            .Color(CLIEnvironment.Operation);

        public override FormattedText Syntax => ComposeAliases();

        public override FormattedText Usage => FormattedText.New
            .AppendLine(Syntax)
            .AppendLine(FormattedNameAndDescription(CLIEnvironment.Operation))
            .Append(ArgumentsUsage());
        protected virtual FormattedText ArgumentsUsage() => IndentChildren("Arguments:", CLIEnvironment.Argument, Arguments.Select(a => a.Usage));

        public virtual Dictionary<string, List<Value>> Parse(ArgumentCrawler crawler)
        {
            ParsedOperationArguments parsedResults = new ParsedOperationArguments(AliasMap());
            try
            {
                parsedResults.Parse(crawler);
            }
            catch (KeyNotFoundException)
            {
                crawler.StepBackwards();
            }
            return parsedResults.Provided;
        }
        private Dictionary<string, Argument> AliasMap()
        {
            Dictionary<string, Argument> toReturn = new Dictionary<string, Argument>(StringComparer.OrdinalIgnoreCase);
            foreach (Argument argument in Arguments)
                foreach (string alias in argument.Aliases)
                    toReturn.Add($"{Argument.ArgumentIdentifier}{alias}", argument);
            return toReturn;
        }
    }
}
