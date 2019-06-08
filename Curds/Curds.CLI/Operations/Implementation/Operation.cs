using System;
using System.Collections.Generic;
using System.Linq;

namespace Curds.CLI.Operations.Implementation
{
    using CLI.Abstraction;
    using CLI.Domain;
    using Domain;
    using Formatting;
    using Formatting.Text.Domain;

    public abstract class Operation : AliasedOptionValue
    {
        public const string OperationIdentifier = "-";
        public static string PrependIdentifier(string value) => $"{OperationIdentifier}{value}";
        public override string AliasPrefix => OperationIdentifier;

        protected abstract IEnumerable<Argument> Arguments { get; }

        protected override Formatted ComposeAliases() => base.ComposeAliases()
            .Color(CLIEnvironment.Operation);

        public override Formatted Syntax => ComposeAliases();

        public override Formatted Usage => new Formatted()
            .AppendLine(Syntax)
            .AppendLine(FormattedNameAndDescription(CLIEnvironment.Operation))
            .Append(ArgumentsUsage());
        protected virtual Formatted ArgumentsUsage() => IndentChildren("Arguments:", CLIEnvironment.Argument, Arguments.Select(a => a.Usage));

        public virtual Dictionary<string, List<Value>> Parse(IArgumentCrawler crawler)
        {
            ParsedArguments parsedResults = new ParsedArguments(AliasMap());
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
