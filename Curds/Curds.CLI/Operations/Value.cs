using System;

namespace Curds.CLI.Operations
{
    using Formatting;
    using Formatting.Tokens;

    public abstract class Value : OptionValue
    {
        public const string SyntaxStart = "<";
        public const string SyntaxEnd = ">";

        public override FormattedText Syntax => EncloseIdentifiers(SyntaxStart, SyntaxEnd, Name);
        public override FormattedText Usage => FormattedNameAndDescription(CLIEnvironment.Value);

        public string RawValue { get; private set; }

        public Value Parse(ArgumentCrawler crawler)
        {
            RawValue = crawler.Parse();
            if (!crawler.AtEnd)
                crawler.Next();
            return this;
        }
    }
}
