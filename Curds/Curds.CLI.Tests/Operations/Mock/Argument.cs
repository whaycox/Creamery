using System.Collections.Generic;

namespace Curds.CLI.Operations.Mock
{
    using CLI.Domain;

    public class Argument : Implementation.Argument
    {
        public bool TestRequired = true;
        public override bool Required => TestRequired;

        public override List<Implementation.Value> Values => new List<Implementation.Value>
        {
            new Value(),
            new Value(),
            new Value(),
        };

        public override IEnumerable<string> Aliases => new List<string>
        {
            nameof(Argument),
            $"{nameof(Argument)}{nameof(Aliases)}",
        };

        protected override string BaseName => nameof(Argument);
        public override string Description => nameof(Description);

        public Argument()
        { }

        public Argument(bool isRequired)
        {
            TestRequired = isRequired;
        }
    }
}
