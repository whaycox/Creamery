using System.Collections.Generic;

namespace Curds.Persistence.Query.Abstraction
{
    public interface ISqlQueryFormatter
    {
        string FormatTokens(IEnumerable<ISqlQueryToken> tokens);
    }
}
