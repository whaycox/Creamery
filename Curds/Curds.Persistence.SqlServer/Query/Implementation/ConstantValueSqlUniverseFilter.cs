using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Query.Implementation
{
    using Abstraction;
    using Model.Abstraction;
    using Domain;

    internal class ConstantValueSqlUniverseFilter : BaseSqlUniverseFilter
    {
        private ISqlColumn Column { get; }
        private object Value { get; }

        public ConstantValueSqlUniverseFilter(
            SqlBooleanOperation operation,
            ISqlColumn column, 
            object value)
            : base(operation)
        {
            Column = column;
            Value = value;
        }

        public override ISqlQueryToken Left(ISqlQueryTokenFactory tokenFactory) => tokenFactory.QualifiedObjectName(Column);
        public override ISqlQueryToken Right(ISqlQueryTokenFactory tokenFactory) => tokenFactory.Parameter(Column.Name, Value);
    }
}
