using System;
using System.Collections.Generic;
using System.Text;
using Curds.Application;
using Curds.Application.Message;
using Curds.CLI.Formatting;

namespace Curds.CLI.Operations
{
    public abstract class BooleanOperation : ArgumentlessOperation
    {
        public override List<Value> Values => new List<Value>();

        protected override FormattedText ValueSyntax() => FormattedText.New;
        protected override FormattedText ValuesUsage() => FormattedText.New;

        public override Dictionary<string, List<Value>> Parse(ArgumentCrawler crawler) => new Dictionary<string, List<Value>>();
    }
}
