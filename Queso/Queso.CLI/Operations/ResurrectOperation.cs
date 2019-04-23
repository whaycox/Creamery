using System;
using System.Collections.Generic;
using System.Text;
using Curds.CLI.Formatting;
using Curds.CLI.Operations;
using Queso.Application.Message.Command.Character;
using Queso.Application;

namespace Queso.CLI.Operations
{
    public class ResurrectOperation : ArgumentlessOperation
    {
        private static readonly List<string> _aliases = new List<string>
        {
            "r",
            "res",
            "rez",
        };

        public override string Name => "Resurrect";
        public override string Description => "Bring a dead hardcore character back to life.";

        public override IEnumerable<string> Aliases => _aliases;

        public override List<Value> Values => new List<Value>
        {
            new CharacterPathValue(),
        };
    }
}
