using System;
using System.Collections.Generic;
using System.Text;
using Curds.CLI.Operations;
using Curds.CLI.Formatting;

namespace Curds.Domain.CLI.Operations
{
    using Application;
    using Application.Message.Query;

    public class MockArgumentlessOperation : Operation<MockApplication>
    {
        public override string Name => throw new NotImplementedException();

        public override string Description => throw new NotImplementedException();

        public override IEnumerable<string> Aliases => throw new NotImplementedException();

        protected override IEnumerable<Argument> Arguments => throw new NotImplementedException();

        public MockArgumentlessOperation(MockQueryDefinition query)
            : base(query)
        { }

        protected override string ArgumentSyntax()
        {
            throw new NotImplementedException();
        }
    }
}
