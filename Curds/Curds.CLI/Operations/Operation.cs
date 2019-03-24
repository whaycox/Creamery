using Curds.Application;
using Curds.Application.Message;
using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.CLI.Operations
{
    using Formatting;

    public abstract class Operation<T> where T : CurdsApplication
    {
        public const string OperationIdentifier = "-";
        public const string AliasStart = "{";
        public const string AliasSeparator = " | ";
        public const string AliasEnd = "}";

        protected BaseMessageDefinition<T> Definition { get; }

        protected abstract IEnumerable<Argument> Arguments { get; }

        public abstract IEnumerable<string> Aliases { get; }
        public FormattedText Usage
        {
            get
            {
                FormattedText toReturn = new FormattedText();
                toReturn.AddLine(PlainTextToken.Create(NameAndSyntax));
                toReturn.AddLine(PlainTextToken.Create(Definition.Description));
                toReturn.Add(ArgumentsUsage());
                return toReturn;
            }
        }

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

        protected string NameAndSyntax => $"{Definition.Name} {ComposedAliases()} {ArgumentSyntax()}";
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
            return builder.ToString();
        }
        protected abstract string ArgumentSyntax();

        protected abstract FormattedText ArgumentsUsage();

        //public override string Syntax
        //{
        //    get
        //    {
        //        StringBuilder aliases = new StringBuilder();
        //        aliases.Append(AliasStart);
        //        int aliasCount = 0;
        //        foreach (string alias in OperationAliases)
        //        {
        //            if (aliasCount++ > 0)
        //                aliases.Append(AliasSeparator);
        //            aliases.Append($"{OperationIdentifier}{alias}");
        //        }
        //        aliases.Append(AliasEnd);
        //        return aliases.ToString();
        //    }
        //}

        //public override void Append(IIndentedWriter writer)
        //{
        //    AppendDescriptionAndAliases(writer);
        //    AppendArguments(writer);
        //}
        //private void AppendDescriptionAndAliases(IIndentedWriter writer)
        //{
        //    writer.AddLine($"{Name} {Syntax}");
        //    writer.AddLine(Description);
        //}
        //protected virtual void AppendArguments(IIndentedWriter writer)
        //{
        //    writer.AddLine("Arguments:");
        //    using (var scope = writer.Scope())
        //        foreach (Argument arg in Arguments)
        //            arg.Append(writer);
        //}
    }
}
