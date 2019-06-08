using System.Collections.Generic;
using System.Linq;

namespace Curds.CLI.Operations.Implementation
{
    using CLI.Abstraction;
    using CLI.Domain;
    using Domain;
    using Formatting;
    using Formatting.Text.Domain;
    using Formatting.Token.Abstraction;

    public abstract class Argument : AliasedOptionValue
    {
        public const string OptionalStart = "[";
        public const string OptionalEnd = "]";

        public const string ArgumentIdentifier = "--";
        public static string PrependIdentifier(string argument) => $"{ArgumentIdentifier}{argument}";
        public override string AliasPrefix => ArgumentIdentifier;

        public sealed override string Name => Required ? BaseName : $"{BaseName}{OptionalSuffix}";
        protected abstract string BaseName { get; }
        private string OptionalSuffix => $" (Optional)";

        protected override Formatted ComposeAliases() => base.ComposeAliases()
            .Color(CLIEnvironment.Argument);

        public override Formatted Syntax
        {
            get
            {
                Formatted basicSyntax = new Formatted()
                    .Concatenate(null, " ", null, new List<IToken> { ComposeAliases(), ValueSyntax() });
                if (!Required)
                    return EncloseIdentifiers(OptionalStart, OptionalEnd, basicSyntax);
                else
                    return basicSyntax;
            }
        }
        private Formatted ValueSyntax() => new Formatted()
            .Concatenate(null, " ", null, Values.Select(v => v.Syntax))
            .Color(CLIEnvironment.Value);

        public override Formatted Usage => new Formatted()
            .AppendLine(Syntax)
            .AppendLine(FormattedNameAndDescription(CLIEnvironment.Argument))
            .Append(ValuesUsage());
        private Formatted ValuesUsage() => IndentChildren("Values:", CLIEnvironment.Value, Values.Select(v => v.Usage));

        public abstract bool Required { get; }
        public abstract List<Value> Values { get; }

        public virtual List<Value> Parse(IArgumentCrawler crawler)
        {
            List<Value> toReturn = Values;
            for (int i = 0; i < toReturn.Count; i++)
                toReturn[i].Parse(crawler);
            return toReturn;
        }
    }
}
