using System;
using System.Collections.Generic;
using System.Text;
using Curds.Application.Text;
using Curds.CLI.Operations;

namespace Queso.CLI.Operations
{
    public class CharacterPathValue : Value
    {
        public override string Name => "character-path";

        public override string Description => "Path to the .d2s character file.";
    }
}
