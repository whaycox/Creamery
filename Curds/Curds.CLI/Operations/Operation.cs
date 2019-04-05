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

    public abstract class Operation<T> : AliasedOptionValue where T : CurdsApplication
    {
        public const string OperationIdentifier = "-";
        public override string AliasPrefix => OperationIdentifier;

        protected BaseMessageDefinition<T> Definition { get; }

        protected abstract IEnumerable<Argument> Arguments { get; }

        protected override FormattedText ComposeAliases() => base.ComposeAliases()
            .Color(CLIEnvironment.Operation);

        public override FormattedText Syntax => ComposeAliases();

        public override FormattedText Usage => FormattedText.New
            .AppendLine(Syntax)
            .AppendLine(FormattedNameAndDescription(CLIEnvironment.Operation))
            .Append(ArgumentsUsage());
        protected virtual FormattedText ArgumentsUsage() => IndentChildren("Arguments:", CLIEnvironment.Argument, Arguments.Select(a => a.Usage));

        public Operation(BaseMessageDefinition<T> definition)
        {
            Definition = definition;
        }

        public virtual Dictionary<string, List<Value>> Parse(ArgumentCrawler crawler)
        {
            ParsedOperationArguments parsedResults = new ParsedOperationArguments(AliasMap());
            try
            {
                parsedResults.Parse(crawler);
            }
            catch (KeyNotFoundException)
            { }
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
