using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Domain.Persistence.EFCore.Seeds
{
    public static class DefinitionArgument
    {
        public static Check.DefinitionArgument[] Data => Check.MockDefinitionArgument.Samples;
    }
}
