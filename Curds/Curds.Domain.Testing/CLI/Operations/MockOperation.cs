using System;
using System.Collections.Generic;
using System.Text;
using Curds.CLI.Operations;
using Curds.CLI.Formatting;

namespace Curds.Domain.CLI.Operations
{
    using Application;
    using Application.Message.Command;

    public class MockOperation : Operation
    {
        public override IEnumerable<string> Aliases => new string[] { nameof(MockOperation), $"{nameof(MockOperation)}{nameof(Aliases)}" };

        public override string Name => nameof(MockOperation);
        public override string Description => $"{nameof(MockOperation)}{nameof(Description)}";

        protected override IEnumerable<Argument> Arguments => new List<Argument>
        {
            new MockArgument(OptionalIdentifier, false),
            new MockArgument(RequiredIdentifier, true),
            new MockBooleanArgument(),
        };
        public const int OptionalIdentifier = 1;
        public const int RequiredIdentifier = 2;
    }
}
