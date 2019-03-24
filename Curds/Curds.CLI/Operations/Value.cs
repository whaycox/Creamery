using System;
using System.Collections.Generic;
using System.Text;
using Curds.CLI.Formatting;

namespace Curds.CLI.Operations
{
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

        public override FormattedText Usage
        {
            get
            {
                FormattedText usage = new FormattedText();
                usage.AddLine(PlainTextToken.Create(ToString()));
                return usage;
            }
        }

        public override string ToString() => $"{Name}: {Description}";
    }
}
