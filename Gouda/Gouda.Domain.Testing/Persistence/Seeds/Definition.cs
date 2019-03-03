using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Domain.Persistence.Seeds
{
    public static class Definition
    {
        public static Check.Definition[] Data => new Check.Definition[]
        {
            Check.MockDefinition.Sample,
        };
    }
}
