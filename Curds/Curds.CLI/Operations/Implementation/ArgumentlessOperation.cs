using Curds.CLI.Formatting;
using System.Collections.Generic;
using System.Linq;

namespace Curds.CLI.Operations.Implementation
{
    using CLI.Domain;
    using CLI.Abstraction;
    using CLI.Implementation;
    using Formatting.Text.Domain;
    using Formatting.Token.Abstraction;

    public abstract class ArgumentlessOperation : Operation
    {
        public const string ArgumentlessKey = nameof(ArgumentlessKey);

        protected sealed override IEnumerable<Argument> Arguments => new List<Argument>();

        public abstract List<Value> Values { get; }

        public override Formatted Syntax => new Formatted()
            .Concatenate(null, " ", null, new List<IToken> { ComposeAliases(), ValueSyntax() });
        protected virtual Formatted ValueSyntax() => new Formatted()
            .Concatenate(null, " ", null, Values.Select(v => v.Syntax))
            .Color(CLIEnvironment.Value);

        public override Formatted Usage => new Formatted()
            .AppendLine(Syntax)
            .AppendLine(FormattedNameAndDescription(CLIEnvironment.Operation))
            .Append(ValuesUsage());
        protected virtual Formatted ValuesUsage() => IndentChildren("Values:", CLIEnvironment.Value, Values.Select(v => v.Usage));

        public override Dictionary<string, List<Value>> Parse(IArgumentCrawler crawler)
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
