using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Query.Implementation
{
    using Abstraction;
    using Domain;

    public abstract class BaseSqlUniverseFilter : ISqlUniverseFilter
    {
        public SqlBooleanOperation Operation { get; set; }

        public BaseSqlUniverseFilter(SqlBooleanOperation operation)
        {
            Operation = operation;
        }

        public abstract ISqlQueryToken Left(ISqlQueryTokenFactory tokenFactory);
        public abstract ISqlQueryToken Right(ISqlQueryTokenFactory tokenFactory);
    }
}
