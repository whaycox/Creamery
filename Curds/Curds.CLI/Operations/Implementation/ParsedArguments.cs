using System;
using System.Collections.Generic;
using System.Linq;

namespace Curds.CLI.Operations.Implementation
{
    using CLI.Abstraction;
    using Exceptions;

    public class ParsedArguments
    {
        public Dictionary<string, List<Value>> Provided { get; } = new Dictionary<string, List<Value>>();
        private Dictionary<string, Argument> AliasMap { get; }

        private int DistinctArguments { get; }
        private bool AllArgumentsParsed => Provided.Count == DistinctArguments;

        public ParsedArguments(Dictionary<string, Argument> aliasMap)
        {
            AliasMap = aliasMap;
            DistinctArguments = AliasMap.GroupBy(a => a.Value).Count();
        }

        public void Parse(IArgumentCrawler crawler)
        {
            while (!crawler.FullyConsumed && !AllArgumentsParsed)
                ParseArgument(crawler);
            if (RequiredArgumentsMissing())
                throw new InvalidOperationException("Not all required arguments have been provided");
        }
        private bool RequiredArgumentsMissing()
        {
            IEnumerable<Argument> requiredArguments = AliasMap
                .GroupBy(g => g.Value)
                .Where(a => a.Key.Required)
                .Select(a => a.Key);
            return requiredArguments.Where(r => !Provided.ContainsKey(r.Name)).Any();
        }
        private void ParseArgument(IArgumentCrawler crawler)
        {
            string argument = crawler.Consume();
            if (!AliasMap.TryGetValue(argument, out Argument selected))
                throw new KeyNotFoundException($"Unexpected argument {argument}");
            if (Provided.ContainsKey(selected.Name))
                throw new DuplicateArgumentException(selected.Name);
            Provided.Add(selected.Name, selected.Parse(crawler));
        }
    }
}
