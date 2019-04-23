using System;
using System.Collections.Generic;
using System.Text;
using Curds.CLI.Operations;

namespace Curds.Domain.CLI.Operations
{
    public class MockValue : Value
    {
        public override string Name => nameof(MockValue);
        public override string Description => $"{nameof(MockValue)}{nameof(Description)}";
    }
}
