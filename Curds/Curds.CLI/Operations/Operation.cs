using Curds.Application;
using Curds.Application.Message;
using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.CLI.Operations
{
    using Formatting;
    using Formatting.Tokens;

    public abstract class Operation<T> where T : CurdsApplication
    {
        public const string OperationIdentifier = "-";
        public const string AliasStart = "{";
        public const string AliasSeparator = " | ";
        public const string AliasEnd = "}";

        protected BaseMessageDefinition<T> Definition { get; }

        protected abstract IEnumerable<Argument> Arguments { get; }

        public abstract IEnumerable<string> Aliases { get; }
        public FormattedText Usage => FormattedText.New
            .Append(Syntax)
            .Append(OperationDescription)
            .Append(ArgumentsUsage());

        private bool AllArgumentsParsed => throw new NotImplementedException();

        public Operation(BaseMessageDefinition<T> definition)
        {
            Definition = definition;
        }

        public virtual Dictionary<string, List<Value>> Parse(ArgumentCrawler crawler)
        {
            Dictionary<string, List<Value>> provided = new Dictionary<string, List<Value>>();
            var aliasMap = AliasMap();
            while (crawler.AtEnd && AllArgumentsParsed)
            {
                Argument argument = LookupArgument(aliasMap, crawler);
                crawler.Next();
                provided.Add(argument.Name, argument.Parse(crawler));
            }
            return provided;
        }
        private Dictionary<string, Argument> AliasMap()
        {
            Dictionary<string, Argument> toReturn = new Dictionary<string, Argument>(StringComparer.OrdinalIgnoreCase);
            foreach (Argument argument in Arguments)
                foreach (string alias in argument.ArgumentAliases)
                    toReturn.Add(alias, argument);
            return toReturn;
        }
        private Argument LookupArgument(Dictionary<string, Argument> aliasMap, ArgumentCrawler crawler)
        {
            string current = crawler.Parse();
            if (!aliasMap.ContainsKey(current))
                throw new KeyNotFoundException($"Unexpected argument alias {current}");
            return aliasMap[current];
        }

        protected FormattedText Syntax => FormattedText.New
            .Color(CLIEnvironment.Operations, PlainTextToken.Create(ComposedAliases()))
            .ColorLine(CLIEnvironment.Value, PlainTextToken.Create(ArgumentSyntax()));
        private string ComposedAliases()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(AliasStart);
            int aliasCount = 0;
            foreach(string alias in Aliases)
            {
                if (aliasCount++ > 0)
                    builder.Append(AliasSeparator);
                builder.Append($"{OperationIdentifier}{alias}");
            }
            builder.Append(AliasEnd);
            builder.Append(" ");
            return builder.ToString();
        }
        protected abstract string ArgumentSyntax();

        private FormattedText OperationDescription => FormattedText.New
            .Color(CLIEnvironment.Operations, PlainTextToken.Create($"{Definition.Name}"))
            .AppendLine(PlainTextToken.Create($": {Definition.Description}"));

        protected abstract FormattedText ArgumentsUsage();
    }
}
