using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Curds.CLI.Operations
{
    public class ParsedOperationArguments
    {
        public Dictionary<string, List<Value>> Provided { get; } = new Dictionary<string, List<Value>>();
        private Dictionary<string, Argument> AliasMap { get; }

        private int DistinctArguments { get; }
        private bool AllArgumentsParsed => Provided.Count == DistinctArguments;

        public ParsedOperationArguments(Dictionary<string, Argument> aliasMap)
        {
            AliasMap = aliasMap;
            DistinctArguments = AliasMap.GroupBy(a => a.Value).Count();
        }

        public void Parse(ArgumentCrawler crawler)
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
        private void ParseArgument(ArgumentCrawler crawler)
        {
            string argument = crawler.Consume();
            if (!AliasMap.TryGetValue(argument, out Argument selected))
                throw new KeyNotFoundException($"Unexpected argument {argument}");
            Provided.Add(selected.Name, selected.Parse(crawler));
        }
    }
}
