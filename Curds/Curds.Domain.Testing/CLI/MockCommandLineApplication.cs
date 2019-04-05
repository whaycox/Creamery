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
            throw new NotImplementedException();
        }
    }
}
