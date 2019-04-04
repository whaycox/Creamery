using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Curds.CLI.Operations
{
    using Formatting;
    using Formatting.Tokens;

    public abstract class AliasedOptionValue : OptionValue
    {
        public const string AliasStart = "{ ";
        public const string AliasSeparator = " | ";
        public const string AliasEnd = " }";

        public abstract string AliasPrefix { get; }
        public abstract IEnumerable<string> Aliases { get; }

        protected virtual FormattedText ComposeAliases() => FormattedText.New
            .Concatenate(AliasStart, AliasSeparator, AliasEnd, Aliases.Select(a => PlainTextToken.Create($"{AliasPrefix}{a}")));
    }
}
