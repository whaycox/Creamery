using System.Collections.Generic;

namespace Curds.CLI.Operations.Mock
{
    public class BooleanOperation : Implementation.BooleanOperation
    {
        public override IEnumerable<string> Aliases => new List<string>
        {
            nameof(BooleanOperation),
            $"{nameof(BooleanOperation)}{nameof(Aliases)}",
        };

        public override string Name => nameof(BooleanOperation);
        public override string Description => nameof(Description);
    }
}
