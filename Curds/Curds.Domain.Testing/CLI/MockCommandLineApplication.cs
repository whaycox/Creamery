using System;
using System.Collections.Generic;
using System.Text;
using Curds.CLI;
using Curds.CLI.Operations;

namespace Curds.Domain.CLI
{
    using Application;
    using Operations;

    public class MockCommandLineApplication : CommandLineApplication<MockApplication>
    {
        public static string ExecutionMessage(string operationName) => $"{operationName} has executed";

        protected override IEnumerable<Operation<MockApplication>> Operations => new List<Operation<MockApplication>>
        {
            new MockOperation(Application.Dispatch.MockCommand),
            new MockArgumentlessOperation(Application.Dispatch.MockQuery),
            new MockBooleanOperation(Application.Dispatch.MockQuery),
        };

        public MockCommandLineApplication(MockApplication application, IConsoleWriter writer)
            : base(application, writer)
        { }

        protected override void ExecuteOperation(OperationParser<MockApplication>.ParsedPair parsedPair)
        {
            switch (parsedPair.Operation)
            {
                case MockBooleanOperation booleanOperation:
                    Writer.Write(ExecutionMessage(nameof(MockBooleanOperation)));
                    break;
                default:
                    throw new InvalidOperationException($"Unexpected operation {parsedPair.Operation.GetType().FullName}");
            }
        }
    }
}
