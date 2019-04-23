using System;
using System.Collections.Generic;
using System.Text;
using Curds.CLI.Formatting;
using Curds.CLI.Operations;

namespace Curds.Domain.CLI.Operations
{
    public class MockArgument : Argument
    {
        public static string IdentifiedName(int identifier) => $"{identifier}{nameof(MockArgument)}";

        public override IEnumerable<string> Aliases => new string[] { Name, $"{Identifier}{nameof(Aliases)}" };

        private int Identifier { get; }

        public override bool Required { get; }

        public override List<Value> Values => new List<Value>
        {
            new MockValue(),
        };

        public override string Name => IdentifiedName(Identifier);
        public override string Description => $"{nameof(MockArgument)}{nameof(Description)}";

        public MockArgument(int identifier, bool required)
        {
            Identifier = identifier;
            Required = required;
        }
    }
}
