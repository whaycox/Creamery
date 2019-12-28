using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Abstraction
{
    public interface ICheck
    {
        Guid ID { get; }
        string Name { get; }
    }
}
