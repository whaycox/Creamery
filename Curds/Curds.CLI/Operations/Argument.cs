using System;
using System.Collections.Generic;
using System.Linq;

namespace Curds.CLI.Operations
{
    using Formatting;
    using Formatting.Tokens;

    public abstract class Argument : AliasedOptionValue
    {
        public const string ArgumentIdentifier = "--";
        public static string PrependIdentifier(string argument) => $"{ArgumentIdentifier}{argument}";
        public override string AliasPrefix => ArgumentIdentifier;

        protected override FormattedText ComposeAliases() => base.ComposeAliases()
            .Color(CLIEnvironment.Argument);

        public override FormattedText Syntax => FormattedText.New
            .Concatenate(null, " ", null, new List<BaseTextToken> { ComposeAliases(), ValueSyntax() });
        private FormattedText ValueSyntax() => FormattedText.New
            .Concatenate(null, " ", null, Values.Select(v => v.Syntax))
            .Color(CLIEnvironment.Value);

        public override FormattedText Usage => FormattedText.New
            .AppendLine(Syntax)
            .AppendLine(FormattedNameAndDescription(CLIEnvironment.Argument))
            .Append(ValuesUsage());
        protected virtual FormattedText ValuesUsage() => IndentChildren("Values:", CLIEnvironment.Value, Values.Select(v => v.Usage));

        public abstract bool Required { get; }
        public abstract List<Value> Values { get; }

        public virtual List<Value> Parse(ArgumentCrawler crawler)
        {
            List<Value> toReturn = Values;
            for (int i = 0; i < toReturn.Count; i++)
                toReturn[i].Parse(crawler);
            return toReturn;
        }
    }
}
