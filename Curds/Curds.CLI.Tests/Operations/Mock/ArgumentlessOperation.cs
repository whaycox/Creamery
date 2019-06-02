using System.Collections.Generic;

namespace Curds.CLI.Operations.Mock
{
    public class ArgumentlessOperation : Implementation.ArgumentlessOperation
    {
        public override List<Implementation.Value> Values => new List<Implementation.Value>
        {
            new Value(),
            new Value(),
            new Value(),
        };

        public override IEnumerable<string> Aliases => new List<string>
        {
            nameof(ArgumentlessOperation),
            $"{nameof(ArgumentlessOperation)}{nameof(Aliases)}",
        };

        public override string Name => nameof(ArgumentlessOperation);
        public override string Description => nameof(Description);
    }
}
