using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Curds.CLI.Implementation
{
    using Abstraction;
    using Application.Domain;
    using Operations.Implementation;

    public abstract class CommandLineApplication<T> where T : CurdsApplication
    {
        private OperationParser Parser = new OperationParser();
        private UsageOperation Help = new UsageOperation();

        protected IConsole Console { get; }

        protected T Application { get; }

        public CommandLineApplication(T application, IConsole console)
        {
            Application = application;
            Console = console;
        }

        private List<Operation> Operations => BuildOperations(new List<Operation>());
        protected virtual List<Operation> BuildOperations(List<Operation> operations)
        {
            operations.Add(Help);
            return operations;
        }

        public void Execute(string[] args)
        {
            try
            {
                ExecuteOperations(Parser.Parse(Operations, args)).AwaitResult();
                Console.Exit(0);
            }
            catch (Exception ex)
            {
                Usage(1, $"Failed to {nameof(Execute)}: {ex}");
            }
        }
        private async Task ExecuteOperations(List<OperationParser.ParsedPair> parsedPairs)
        {
            foreach (var pair in parsedPairs)
                await ExecuteOperation(pair);
        }
        protected abstract Task ExecuteOperation(OperationParser.ParsedPair parsedPair);
        private void Usage(int exitCode, string message, params object[] args) => Usage(exitCode, string.Format(message, args));
        private void Usage(int exitCode, string message)
        {
            Help.Format(message, Application.Description, Operations).Write(Console);
            Console.Exit(exitCode);
        }
    }
}
