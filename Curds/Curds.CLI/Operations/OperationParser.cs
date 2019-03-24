using Curds.Application;
using System.Collections.Generic;
using System;

namespace Curds.CLI.Operations
{
    using Formatting;

    public class OperationParser<T> where T : CurdsApplication
    {
        public List<ParsedPair> Parse(IEnumerable<Operation<T>> operations, string[] args)
        {
            ArgumentCrawler crawler = new ArgumentCrawler(args);
            Dictionary<string, Operation<T>> aliasMap = OperationAliasMap(operations);

            List<ParsedPair> parsedOperations = new List<ParsedPair>();
            while (!crawler.AtEnd)
            {
                if (!aliasMap.TryGetValue(crawler.Parse(), out Operation<T> selected))
                    throw new KeyNotFoundException($"Unexpected operation {crawler.Parse()}");
                crawler.Next();
                parsedOperations.Add(new ParsedPair(selected, selected.Parse(crawler)));
            }
            return parsedOperations;
        }
        private Dictionary<string, Operation<T>> OperationAliasMap(IEnumerable<Operation<T>> operations)
        {
            Dictionary<string, Operation<T>> toReturn = new Dictionary<string, Operation<T>>();
            foreach (Operation<T> operation in operations)
                foreach (string alias in operation.Aliases)
                    toReturn.Add($"{Operation<T>.OperationIdentifier}{alias}", operation);
            return toReturn;
        }

        public FormattedText OperationUsages(IEnumerable<Operation<T>> operations)
        {
            FormattedText toReturn = new FormattedText();
            toReturn.AddLine(PlainTextToken.Create("Operations:"));
            using (IndentedText indented = new IndentedText())
            {
                foreach (Operation<T> operation in operations)
                    indented.Add(operation.Usage);
                toReturn.Add(indented);
            }
            return toReturn;
        }

        public class ParsedPair
        {
            public Operation<T> Operation { get; }
            public Dictionary<string, List<Value>> Arguments { get; }

            public ParsedPair(Operation<T> operation, Dictionary<string, List<Value>> arguments)
            {
                Operation = operation;
                Arguments = arguments;
            }
        }
    }
}
