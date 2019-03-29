using System;
using System.Collections.Generic;
using System.Text;
using Curds.CLI.Operations;
using Curds.CLI.Formatting;

namespace Curds.Domain.CLI.Operations
{
    using Application;
    using Application.Message.Command;

    public class MockOperation : Operation<MockApplication>
    {
        public override string Name => throw new NotImplementedException();

        public override string Description => throw new NotImplementedException();

        public override IEnumerable<string> Aliases => throw new NotImplementedException();

        protected override IEnumerable<Argument> Arguments => throw new NotImplementedException();

        public MockOperation(MockCommandDefinition commandDefinition)
            : base(commandDefinition)
        { }

        protected override FormattedText ArgumentsUsage()
        {
            throw new NotImplementedException();
        }

        protected override string ArgumentSyntax()
        {
            throw new NotImplementedException();
        }
    }
}
