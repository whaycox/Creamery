using System;
using System.Collections.Generic;
using System.Text;
using Curds.CLI.Operations;
using Curds.CLI.Formatting;

namespace Curds.Domain.CLI.Operations
{
    using Application;
    using Application.Message.Query;

    public class MockArgumentlessOperation : ArgumentlessOperation<MockApplication>
    {
        public override string Name => nameof(MockArgumentlessOperation);

        public override string Description => $"{nameof(MockArgumentlessOperation)}{nameof(Description)}";

        public override IEnumerable<string> Aliases => new string[] { nameof(MockArgumentlessOperation), $"{nameof(MockArgumentlessOperation)}{nameof(Aliases)}" };

        public override List<Value> Values => new List<Value>
        {
            new MockValue(),
        };

        public MockArgumentlessOperation(MockQueryDefinition query)
            : base(query)
        { }
    }
}
