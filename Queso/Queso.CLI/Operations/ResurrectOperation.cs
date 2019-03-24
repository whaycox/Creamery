using System;
using System.Collections.Generic;
using System.Text;
using Curds.CLI.Formatting;
using Curds.CLI.Operations;
using Queso.Application.Message.Command.Character;
using Queso.Application;

namespace Queso.CLI.Operations
{
    public class ResurrectOperation : ArgumentlessOperation<QuesoApplication>
    {
        private static readonly List<string> _aliases = new List<string>
        {
            "r",
            "res",
            "rez",
        };

        public override IEnumerable<string> Aliases => _aliases;

        public override List<Value> Values => new List<Value>
        {
            new CharacterPathValue(),
        };

        public ResurrectOperation(ResurrectDefinition definition)
            : base(definition)
        { }

    }
}
