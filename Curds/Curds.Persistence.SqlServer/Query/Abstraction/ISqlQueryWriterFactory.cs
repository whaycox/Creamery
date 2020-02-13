using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Query.Abstraction
{
    public interface ISqlQueryWriterFactory
    {
        ISqlQueryWriter Create();
    }
}
