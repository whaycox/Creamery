using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Application.Persistence
{
    public interface ISeedGenerator
    {
        int Next { get; }
    }
}
