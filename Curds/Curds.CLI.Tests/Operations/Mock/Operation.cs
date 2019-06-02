using System.Collections.Generic;

namespace Curds.CLI.Operations.Mock
{
    public class Operation : Implementation.Operation
    {
        public override IEnumerable<string> Aliases => new List<string>
        {
            nameof(Operation),
            $"{nameof(Operation)}{nameof(Aliases)}",
        };

        public override string Name => nameof(Operation);
        public override string Description => nameof(Description);

        public bool ArgumentIsRequired = true;
        public bool BooleanArgumentIsRequired = true;
        protected override IEnumerable<Implementation.Argument> Arguments => new List<Implementation.Argument>
        {
            new Argument(ArgumentIsRequired),
            new BooleanArgument(BooleanArgumentIsRequired),
        };
    }
}
