using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Curds.Persistence.Query.Abstraction
{
    public interface ISqlColumn
    {
        ISqlTable Table { get; }

        string Name { get; }
        SqlDbType Type { get; }
    }
}
