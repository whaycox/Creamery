using System;
using System.Collections.Generic;
using System.Text;
using Curds.Application;
using Curds.Application.Message;
using Curds.CLI.Formatting;

namespace Curds.CLI.Operations
{
    public abstract class BooleanOperation<T> : ArgumentlessOperation<T> where T : CurdsApplication
    {
        public override List<Value> Values => new List<Value>();

        public BooleanOperation(BaseMessageDefinition<T> definition)
            : base(definition)
        { }

        protected override FormattedText ValueSyntax() => FormattedText.New;
        protected override FormattedText ValuesUsage() => FormattedText.New;

        public override Dictionary<string, List<Value>> Parse(ArgumentCrawler crawler) => new Dictionary<string, List<Value>>();
    }
}
