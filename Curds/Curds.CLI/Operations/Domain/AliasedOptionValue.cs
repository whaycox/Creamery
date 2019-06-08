using System.Collections.Generic;
using System.Linq;

namespace Curds.CLI.Operations.Domain
{
    using Formatting;
    using Formatting.Text.Domain;
    using Formatting.Token.Implementation;

    public abstract class AliasedOptionValue : OptionValue
    {
        public const string AliasStart = "{ ";
        public const string AliasSeparator = " | ";
        public const string AliasEnd = " }";

        public abstract string AliasPrefix { get; }
        public abstract IEnumerable<string> Aliases { get; }

        protected virtual Formatted ComposeAliases() => new Formatted()
            .Concatenate(AliasStart, AliasSeparator, AliasEnd, Aliases.Select(a => new PlainText($"{AliasPrefix}{a}")));
    }
}
