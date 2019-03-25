using Curds.Application;
using Curds.Application.Message;
using Curds.CLI.Formatting;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Curds.CLI.Operations
{
    using Formatting.Tokens;

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

        protected override FormattedText ArgumentsUsage() => FormattedText.New
            .ColorLine(CLIEnvironment.Value, Header)
            .Indent(Values.Select(v => v.Usage));
        private BaseTextToken Header => PlainTextToken.Create("Values:");

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
