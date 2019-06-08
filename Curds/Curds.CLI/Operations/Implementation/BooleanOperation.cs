using System.Collections.Generic;

namespace Curds.CLI.Operations.Implementation
{
    using CLI.Abstraction;
    using CLI.Domain;
    using Formatting.Text.Domain;
    using Formatting;

    public abstract class BooleanOperation : ArgumentlessOperation
    {
        public override List<Value> Values => new List<Value>();

        public override Formatted Syntax => ComposeAliases();
        public override Formatted Usage => new Formatted()
            .AppendLine(Syntax)
            .Append(FormattedNameAndDescription(CLIEnvironment.Operation));

        protected override Formatted ValueSyntax() => new Formatted();
        protected override Formatted ValuesUsage() => new Formatted();

        public override Dictionary<string, List<Value>> Parse(IArgumentCrawler crawler) => new Dictionary<string, List<Value>>();
    }
}
