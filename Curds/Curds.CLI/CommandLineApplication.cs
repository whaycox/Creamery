using Curds.Application;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Curds.CLI
{
    using Operations;

    public abstract class CommandLineApplication<T> where T : CurdsApplication
    {
        protected static string ImplicitArgument = ArgumentlessOperation.ArgumentlessKey;

        private OperationParser<T> Parser = new OperationParser<T>();
        private UsageOperation Help = new UsageOperation();

        protected IConsoleWriter Writer { get; }

        protected IEnumerable<Operation> Operations { get; }

        protected T Application { get; }

        public CommandLineApplication(T application, IConsoleWriter writer)
        {
            Application = application;
            Writer = writer;

            Operations = BuildOperations(new List<Operation>());
        }

        protected virtual List<Operation> BuildOperations(List<Operation> operations)
        {
            operations.Add(Help);
            return operations;
        }

        public void Execute(string[] args)
        {
            if (args == null || args.Length == 0)
                Usage(1, "Please provide arguments");
            try
            {
                ExecuteOperations(Parser.Parse(Operations, args)).AwaitResult();
            }
            catch (Exception ex)
            {
                Usage(1, $"Failed to {nameof(Execute)}: {ex}");
            }
        }
        private async Task ExecuteOperations(List<OperationParser<T>.ParsedPair> parsedPairs)
        {
            foreach (var pair in parsedPairs)
                await ExecuteOperation(pair);
        }
        protected abstract Task ExecuteOperation(OperationParser<T>.ParsedPair parsedPair);
        private void Usage(int exitCode, string message, params object[] args) => Usage(exitCode, string.Format(message, args));
        private void Usage(int exitCode, string message)
        {
            Help.Format(message, Application.Description, Operations).Write(Writer);
            Writer.Exit(exitCode);
        }
    }
}
