using System;

namespace Curds.CLI.Operations.Implementation
{
    using Formatting;
    using Formatting.Text.Domain;
    using Domain;
    using CLI.Abstraction;
    using CLI.Domain;
    using CLI.Implementation;

    public abstract class Value : OptionValue
    {
        public const string SyntaxStart = "<";
        public const string SyntaxEnd = ">";

        public override Formatted Syntax => EncloseIdentifiers(SyntaxStart, SyntaxEnd, Name);
        public override Formatted Usage => FormattedNameAndDescription(CLIEnvironment.Value);

        public string RawValue { get; private set; }

        public Value Parse(IArgumentCrawler crawler)
        {
            RawValue = crawler.Consume();
            return this;
        }
    }
}
