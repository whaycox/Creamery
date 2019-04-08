using Curds.Application;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Curds.CLI.Operations
{
    using Formatting;
    using Formatting.Tokens;

    public class OperationParser<T> where T : CurdsApplication
    {
        public List<ParsedPair> Parse(IEnumerable<Operation<T>> operations, string[] args)
        {
            ArgumentCrawler crawler = new ArgumentCrawler(args);
            Dictionary<string, Operation<T>> aliasMap = OperationAliasMap(operations);
            List<ParsedPair> parsedOperations = new List<ParsedPair>();

            while (!crawler.FullyConsumed)
                parsedOperations.Add(BuildPair(aliasMap, crawler));
            return parsedOperations;
        }
        private ParsedPair BuildPair(Dictionary<string, Operation<T>> aliasMap, ArgumentCrawler crawler)
        {
            string operation = crawler.Consume();
            if (!aliasMap.TryGetValue(operation, out Operation<T> selected))
                throw new KeyNotFoundException($"Unexpected operation {operation}");
            return new ParsedPair(selected, selected.Parse(crawler));
        }
        private Dictionary<string, Operation<T>> OperationAliasMap(IEnumerable<Operation<T>> operations)
        {
            Dictionary<string, Operation<T>> toReturn = new Dictionary<string, Operation<T>>();
            foreach (Operation<T> operation in operations)
                foreach (string alias in operation.Aliases)
                    toReturn.Add(Operation.PrependIdentifier(alias), operation);
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
