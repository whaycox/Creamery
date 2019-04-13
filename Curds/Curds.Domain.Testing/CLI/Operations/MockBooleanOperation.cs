using System;
using System.Collections.Generic;
using System.Text;
using Curds.CLI.Operations;

namespace Curds.Domain.CLI.Operations
{
    using Application;
    using Application.Message.Query;

    public class MockBooleanOperation : BooleanOperation
    {
        public override IEnumerable<string> Aliases => new string[] { nameof(MockBooleanOperation), $"{nameof(MockBooleanOperation)}{nameof(Aliases)}" };

        public override string Name => nameof(MockBooleanOperation);
        public override string Description => $"{nameof(MockBooleanOperation)}{nameof(Description)}";
    }
}
