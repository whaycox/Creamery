using System;
using System.Collections.Generic;
using System.Text;
using Curds.CLI.Operations;
using Queso.Application;
using Queso.Application.Message.Command.Character;

namespace Queso.CLI.Operations
{
    public static class QuesoOperations
    {
        public static IEnumerable<Operation> Operations(QuesoApplication application)
        {
            yield return new ResurrectOperation();
        }

    }
}
