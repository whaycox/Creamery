using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace Curds.Persistence.Query.Abstraction
{
    using Text.Abstraction;

    internal interface ISqlQueryFormatter
    {
        string FormatTokens(IEnumerable<ISqlQueryToken> tokens);
    }
}
