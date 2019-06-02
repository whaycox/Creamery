using System.Collections.Generic;

namespace Curds.CLI.Operations.Mock
{
    public class BooleanArgument : Implementation.BooleanArgument
    {
        public bool TestRequired = true;
        public override bool Required => TestRequired;

        public override IEnumerable<string> Aliases => new List<string>
        {
            nameof(BooleanArgument),
            $"{nameof(BooleanArgument)}{nameof(Aliases)}",
        };

        protected override string BaseName => nameof(BooleanArgument);
        public override string Description => nameof(Description);

        public BooleanArgument()
        { }

        public BooleanArgument(bool isRequired)
        {
            TestRequired = isRequired;
        }
    }
}
