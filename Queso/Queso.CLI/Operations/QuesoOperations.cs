using System;
using System.Collections.Generic;
using System.Text;
using Curds.CLI.Operations;
using Queso.Application;

namespace Queso.CLI.Operations
{
    public static class QuesoOperations
    {
        public static IEnumerable<Operation<QuesoApplication>> Operations(QuesoApplication application)
        {
            yield return new ResurrectOperation(application.Commands.Resurrect);
        }

    }
}
