using Curds.Application;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Curds.CLI
{
    using Formatting;
    using Formatting.Tokens;
    using Operations;
    using Writer;

    public abstract class CommandLineApplication<T> where T : CurdsApplication
    {
        protected static string ImplicitArgument = ArgumentlessOperation<CurdsApplication>.ArgumentlessKey;

        private OperationParser<T> Parser = new OperationParser<T>();

        private IConsoleWriter Writer { get; }

        protected abstract IEnumerable<Operation<T>> Operations { get; }

        protected T Application { get; }

        public CommandLineApplication(T application, IConsoleWriter writer)
        {
            Application = application;
            Writer = writer;
        }

        private void Usage(int exitCode, string message, params object[] args) => Usage(exitCode, string.Format(message, args));
        private void Usage(int exitCode, string message)
        {
            Usage(message).Write(Writer);
            Writer.Exit(exitCode);
        }
        private FormattedText Usage(string message) => FormattedText.New
            .Color(CLIEnvironment.Error, UsageMessage(message))
            .AppendLine(ApplicationDescription)
            .Append(OperationUsages);
        private FormattedText UsageMessage(string message) => FormattedText.New.AppendLine(PlainTextToken.Create(message));
        private FormattedText ApplicationDescription => FormattedText.New
            .Concatenate(null, " | ", null, new List<BaseTextToken> { AppName, PlainTextToken.Create(Application.Description) });
        private FormattedText AppName => FormattedText.New
            .Color(CLIEnvironment.Application, PlainTextToken.Create(AppDomain.CurrentDomain.FriendlyName));
        private FormattedText OperationUsages => FormattedText.New
            .ColorLine(CLIEnvironment.Operation, OperationHeader)
            .IndentLine(Operations.Select(o => o.Usage));
        private BaseTextToken OperationHeader => PlainTextToken.Create("Operations:");

        public void Execute(string[] args)
        {
            if (args == null || args.Length == 0)
                Usage(1, "Please provide arguments");
            try
            {
                ExecuteOperations(Parser.Parse(Operations, args));
            }
            catch (Exception ex)
            {
                Usage(1, $"Failed to {nameof(Execute)}: {ex}");
            }
        }
        private void ExecuteOperations(List<OperationParser<T>.ParsedPair> parsedPairs)
        {
            foreach (var pair in parsedPairs)
                ExecuteOperation(pair);
        }
        protected abstract void ExecuteOperation(OperationParser<T>.ParsedPair parsedPair);
    }
}
