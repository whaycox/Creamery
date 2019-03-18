using System;
using System.Collections.Generic;
using System.Text;
using Curds.Infrastructure.Text;
using Curds.Application.Text;

namespace Queso.CLI.Operations
{
    public static class QuesoOperations
    {
        private static readonly List<Operation> Operations = new List<Operation>
        {
            new ResurrectOperation(),
        };

        public static Dictionary<Operation, Dictionary<string, List<Value>>> Parse(string[] args) => ParseOperations(args, Operations);

        private static Dictionary<Operation, Dictionary<string, List<Value>>> ParseOperations(string[] args, List<Operation> availableOperations)
        {
            ArgumentCrawler crawler = new ArgumentCrawler(args);
            var aliasMaps = AliasMaps(availableOperations);
            Dictionary<Operation, Dictionary<string, List<Value>>> provided = new Dictionary<Operation, Dictionary<string, List<Value>>>();
            while (!crawler.AtEnd)
            {
                Operation matched = LookupOperation(aliasMaps, crawler);
                crawler.Next();
                provided.Add(matched, matched.Parse(crawler));
            }
            return provided;
        }
        private static Dictionary<string, Operation> AliasMaps(List<Operation> availableOperations)
        {
            Dictionary<string, Operation> toReturn = new Dictionary<string, Operation>(StringComparer.OrdinalIgnoreCase);
            foreach (Operation operation in availableOperations)
                foreach (string alias in operation.OperationAliases)
                    toReturn.Add($"{Operation.OperationIdentifier}{alias}", operation);
            return toReturn;
        }
        private static Operation LookupOperation(Dictionary<string, Operation> aliasMaps, ArgumentCrawler crawler)
        {
            string currentArg = crawler.Parse();
            if (!aliasMaps.ContainsKey(currentArg))
                throw new KeyNotFoundException($"Unexpected operation alias {currentArg}");
            return aliasMaps[currentArg];
        }

        public static string OperationDescriptions
        {
            get
            {
                IndentedWriter writer = new IndentedWriter();
                writer.AddLine(FormatApplicationInformation());
                writer.AddLine("Operations:");
                using (var scope = writer.Scope())
                    foreach (Operation operation in Operations)
                        operation.Append(writer);
                return writer.Write;
            }
        }
        private static string FormatApplicationInformation()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append($"{AppDomain.CurrentDomain.FriendlyName} | ");
            builder.Append("A CLI application for Diablo 2 character management");
            return builder.ToString();
        }
    }
}
