using System.Collections.Generic;

namespace Curds.CLI.Operations.Implementation
{
    using CLI.Abstraction;
    using CLI.Domain;
    using Formatting;
    using Formatting.Text.Domain;

    public abstract class BooleanArgument : Argument
    {
        public sealed override List<Value> Values => new List<Value>();

        public override Formatted Syntax
        {
            get
            {
                if (!Required)
                    return EncloseIdentifiers(OptionalStart, OptionalEnd, ComposeAliases());
                else
                    return ComposeAliases();
            }
        }

        public override Formatted Usage => new Formatted()
            .AppendLine(Syntax)
            .Append(FormattedNameAndDescription(CLIEnvironment.Argument));

        public sealed override List<Value> Parse(IArgumentCrawler crawler) => new List<Value>();
    }
}
