using System;
using System.Collections.Generic;
using System.Text;

namespace Queso.CLI.Operations
{
    public class ResurrectOperation : Operation.ArgumentlessOperation
    {
        private static readonly List<string> _aliases = new List<string>
        {
            "r",
            "res",
            "rez",
        };

        public override IEnumerable<string> OperationAliases => _aliases;

        public override string Name => "Resurrect";
        public override string Description => "Bring a dead hardcore character back to life.";

        public override List<Value> Values => new List<Value>
        {
            new CharacterPathValue(),
        };
    }
}
