namespace Curds.CLI.Operations
{
    using Formatting;
    using Formatting.Tokens;

    public abstract class Value : OptionValue
    {
        public const string SyntaxStart = "<";
        public const string SyntaxEnd = ">";

        public override string Syntax => $"{SyntaxStart}{Name}{SyntaxEnd}";

        public string RawValue { get; private set; }

        public Value Parse(ArgumentCrawler crawler)
        {
            RawValue = crawler.Parse();
            return this;
        }

        public override FormattedText Usage => FormattedText.New
            .Color(CLIEnvironment.Value, PlainTextToken.Create(Name))
            .AppendLine(PlainTextToken.Create($": {Description}"));
    }
}
