using Curds.CLI;
using Curds.CLI.Operations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Curds.Domain.CLI
{
    using Application;
    using Operations;

    public class MockCommandLineApplication : CommandLineApplication<MockApplication>
    {
        public static string ExecutionMessage(string operationName) => $"{operationName} has executed";

        public MockCommandLineApplication(MockApplication application, IConsoleWriter writer)
            : base(application, writer)
        { }

        protected override List<Operation> BuildOperations(List<Operation> operations)
        {
            operations = base.BuildOperations(operations);
            operations.Add(new MockOperation());
            operations.Add(new MockArgumentlessOperation());
            operations.Add(new MockBooleanOperation());
            return operations;
        }

        protected override Task ExecuteOperation(OperationParser<MockApplication>.ParsedPair parsedPair) => Task.Factory.StartNew(() => ExecuteInternal(parsedPair));
        private void ExecuteInternal(OperationParser<MockApplication>.ParsedPair parsedPair)
        {
            switch (parsedPair.Operation)
            {
                case MockBooleanOperation booleanOperation:
                    Writer.WriteLine(ExecutionMessage(nameof(MockBooleanOperation)));
                    break;
                case MockArgumentlessOperation argumentlessOperation:
                    foreach (Value value in parsedPair.Arguments[ArgumentlessOperation.ArgumentlessKey])
                        Writer.WriteLine(value.RawValue);
                    Writer.WriteLine(ExecutionMessage(nameof(MockArgumentlessOperation)));
                    break;
                case MockOperation operation:
                    foreach (var arguments in parsedPair.Arguments)
                    {
                        Writer.WriteLine(arguments.Key);
                        foreach (Value value in arguments.Value)
                            Writer.WriteLine(value.RawValue);
                    }
                    Writer.WriteLine(ExecutionMessage(nameof(MockOperation)));
                    break;
                default:
                    throw new InvalidOperationException($"Unexpected operation {parsedPair.Operation.GetType().FullName}");
            }
        }
    }
}
