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
        public const string ArgumentlessKey = nameof(ArgumentlessKey);

        protected sealed override IEnumerable<Argument> Arguments => new List<Argument>();

        public abstract List<Value> Values { get; }

        public ArgumentlessOperation(BaseMessageDefinition<T> definition)
            : base(definition)
        { }

        public override FormattedText Syntax => FormattedText.New
            .Concatenate(null, " ", null, new List<BaseTextToken> { ComposeAliases(), ValueSyntax() });
        protected virtual FormattedText ValueSyntax() => FormattedText.New
            .Concatenate(null, " ", null, Values.Select(v => v.Syntax))
            .Color(CLIEnvironment.Value);

        public override FormattedText Usage => FormattedText.New
            .AppendLine(Syntax)
            .AppendLine(FormattedNameAndDescription(CLIEnvironment.Operation))
            .Append(ValuesUsage());
        protected virtual FormattedText ValuesUsage() => IndentChildren("Values:", CLIEnvironment.Value, Values.Select(v => v.Usage));

        public override Dictionary<string, List<Value>> Parse(ArgumentCrawler crawler)
        {
            List<Value> provided = new List<Value>();
            foreach (Value value in Values)
                provided.Add(value.Parse(crawler));
            return new Dictionary<string, List<Value>>
                {
                    { ArgumentlessKey, provided }
                };
        }
    }
}
