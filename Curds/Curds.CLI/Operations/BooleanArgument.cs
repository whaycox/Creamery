using System;
using System.Collections.Generic;
using System.Text;
using Curds.CLI.Formatting;

namespace Curds.CLI.Operations
{
    public abstract class BooleanArgument : Argument
    {
        public sealed override List<Value> Values => new List<Value>();

        public override FormattedText Syntax => ComposeAliases();

        protected override FormattedText ValuesUsage() => FormattedText.New;

        public sealed override List<Value> Parse(ArgumentCrawler crawler) => new List<Value>();
    }
}
