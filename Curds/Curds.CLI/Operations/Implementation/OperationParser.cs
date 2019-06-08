using System.Collections.Generic;
using System;

namespace Curds.CLI.Operations.Implementation
{
    using Application.Domain;
    using CLI.Implementation;

    public class OperationParser
    {
        public List<ParsedPair> Parse(IEnumerable<Operation> operations, string[] args)
        {
            if (args == null || args.Length == 0)
                throw new InvalidOperationException("Please provide arguments");

            ArgumentCrawler crawler = new ArgumentCrawler(args);
            Dictionary<string, Operation> aliasMap = OperationAliasMap(operations);
            List<ParsedPair> parsedOperations = new List<ParsedPair>();

            while (!crawler.FullyConsumed)
                parsedOperations.Add(BuildPair(aliasMap, crawler));
            return parsedOperations;
        }
        private ParsedPair BuildPair(Dictionary<string, Operation> aliasMap, ArgumentCrawler crawler)
        {
            string operation = crawler.Consume();
            if (!aliasMap.TryGetValue(operation, out Operation selected))
                throw new KeyNotFoundException($"Unexpected operation {operation}");
            return new ParsedPair(selected, selected.Parse(crawler));
        }
        private Dictionary<string, Operation> OperationAliasMap(IEnumerable<Operation> operations)
        {
            Dictionary<string, Operation> toReturn = new Dictionary<string, Operation>();
            foreach (Operation operation in operations)
                foreach (string alias in operation.Aliases)
                    toReturn.Add(Operation.PrependIdentifier(alias), operation);
            return toReturn;
        }

        public class ParsedPair
        {
            public Operation Operation { get; }
            public Dictionary<string, List<Value>> Arguments { get; }

            public ParsedPair(Operation operation, Dictionary<string, List<Value>> arguments)
            {
                Operation = operation;
                Arguments = arguments;
            }
        }
    }
}
