using System;
using System.Collections.Generic;
using System.Text;

namespace Queso.CLI
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
    }
}
