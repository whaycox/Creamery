using System;
using System.Collections.Generic;
using System.Text;
using Curds.Application;
using Curds.Application.Message;
using Curds.CLI.Formatting;

namespace Curds.CLI.Operations
{
    public abstract class ArgumentlessOperation<T> : Operation<T> where T : CurdsApplication
    {
        public const string Argumentless = nameof(Argumentless);

        protected override IEnumerable<Argument> Arguments => new List<Argument>();

        public abstract List<Value> Values { get; }

        public ArgumentlessOperation(BaseMessageDefinition<T> definition)
            : base(definition)
        { }

        protected override string ArgumentSyntax()
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < Values.Count; i++)
            {
                if (i != 0)
                    builder.Append(" ");
                builder.Append(Values[i].Syntax);
            }
            return builder.ToString();
        }

        protected override FormattedText ArgumentsUsage()
        {
            FormattedText toReturn = new FormattedText();
            toReturn.AddLine(PlainTextToken.Create("Values:"));
            using (IndentedText indented = new IndentedText())
            {
                foreach (Value value in Values)
                    indented.Add(value.Usage);
                toReturn.Add(indented);
            }
            return toReturn;
        }

        public override Dictionary<string, List<Value>> Parse(ArgumentCrawler crawler)
        {
            List<Value> provided = new List<Value>();
            foreach (Value value in Values)
                provided.Add(value.Parse(crawler));
            return new Dictionary<string, List<Value>>
                {
                    { Argumentless, provided }
                };
        }
    }
}
