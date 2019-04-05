using System;
using System.Collections.Generic;
using System.Text;
using Curds.CLI.Operations;

namespace Curds.Domain.CLI.Operations
{
    public class MockBooleanArgument : BooleanArgument
    {
        public override bool Required => false;

        public override IEnumerable<string> Aliases => new string[] { nameof(MockBooleanArgument), $"{nameof(MockBooleanArgument)}{nameof(Aliases)}" };

        public override string Name => nameof(MockBooleanArgument);
        public override string Description => $"{nameof(MockBooleanArgument)}{nameof(Description)}";
    }
}
