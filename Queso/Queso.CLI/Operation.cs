using Curds.Application.Text;
using System;
using System.Collections.Generic;
using System.Text;

namespace Queso.CLI
{
    public abstract class Operation : OptionValue
    {
        public const string OperationIdentifier = "-";
        public const string AliasStart = "{";
        public const string AliasSeparator = " | ";
        public const string AliasEnd = "}";

        public abstract IEnumerable<string> OperationAliases { get; }

        public override string Syntax
        {
            get
            {
                StringBuilder aliases = new StringBuilder();
                aliases.Append(AliasStart);
                int aliasCount = 0;
                foreach (string alias in OperationAliases)
                {
                    if (aliasCount++ > 0)
                        aliases.Append(AliasSeparator);
                    aliases.Append($"{OperationIdentifier}{alias}");
                }
                aliases.Append(AliasEnd);
                return aliases.ToString();
            }
        }

        public abstract IEnumerable<Argument> Arguments { get; }

        public override void Append(IIndentedWriter writer)
        {
            AppendDescriptionAndAliases(writer);
            AppendArguments(writer);
        }
        private void AppendDescriptionAndAliases(IIndentedWriter writer)
        {
            writer.AddLine($"{Name} {Syntax}");
            writer.AddLine(Description);
        }
        protected virtual void AppendArguments(IIndentedWriter writer)
        {
            writer.AddLine("Arguments:");
            using (var scope = writer.Scope())
                foreach (Argument arg in Arguments)
                    arg.Append(writer);
        }

        private bool AllArgumentsParsed => throw new NotImplementedException();

        public virtual Dictionary<string, List<Value>> Parse(ArgumentCrawler crawler)
        {
            Dictionary<string, List<Value>> provided = new Dictionary<string, List<Value>>();
            var aliasMap = AliasMap();
            while(crawler.AtEnd && AllArgumentsParsed)
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

        public abstract class ArgumentlessOperation : Operation
        {
            public const string Argumentless = nameof(Argumentless);

            public sealed override IEnumerable<Argument> Arguments => new List<Argument>();

            public abstract List<Value> Values { get; }

            public override string Syntax => $"{base.Syntax} {ValuesSyntax()}";
            private string ValuesSyntax()
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

            protected override void AppendArguments(IIndentedWriter writer)
            {
                writer.AddLine("Values:");
                using (var scope = writer.Scope())
                    foreach (Value value in Values)
                        value.Append(writer);
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
}
