using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Query.Abstraction
{
    using Domain;

    public interface ISqlUniverseFilter
    {
        SqlBooleanOperation Operation { get; }

        ISqlQueryToken Left(ISqlQueryTokenFactory tokenFactory);
        ISqlQueryToken Right(ISqlQueryTokenFactory tokenFactory);

    }
}
