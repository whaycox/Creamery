using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;
using System.Data;

namespace Curds.Persistence.Model.Implementation
{
    using Query.Abstraction;
    using Abstraction;
    using Domain;

    internal delegate Expression PopulateValueDelegate(ParameterExpression entityParameter, IValueModel value, ParameterExpression queryReaderParameter);

    internal abstract class BaseQueryReaderExpressionBuilder : BaseExpressionBuilder
    {
        private static IReadOnlyDictionary<SqlDbType, QueryReaderOption> QueryReaderMap { get; } = new Dictionary<SqlDbType, QueryReaderOption>
        {
            { SqlDbType.NVarChar, QueryReaderOption.String },
            { SqlDbType.Bit, QueryReaderOption.Bool },
            { SqlDbType.TinyInt, QueryReaderOption.Byte },
            { SqlDbType.SmallInt, QueryReaderOption.Short },
            { SqlDbType.Int, QueryReaderOption.Int },
            { SqlDbType.BigInt, QueryReaderOption.Long },
            { SqlDbType.DateTime, QueryReaderOption.DateTime },
            { SqlDbType.DateTimeOffset, QueryReaderOption.DateTimeOffset },
            { SqlDbType.Decimal, QueryReaderOption.Decimal },
            { SqlDbType.Float, QueryReaderOption.Double },
        };

        protected Expression PopulateValueFromReader(ParameterExpression entityParameter, IValueModel value, ParameterExpression queryReaderParameter)
        {
            if (!QueryReaderMap.TryGetValue(value.SqlType, out QueryReaderOption queryReaderOption))
                throw new ModelException($"Unsupported type for {value.Name}, {value.SqlType}");

            PropertyInfo valueProperty = value.Property;
            Type propertyType = valueProperty.PropertyType;
            MethodInfo setMethod = valueProperty.SetMethod;
            Expression readValueExpression = ReadValueFromReader(queryReaderParameter, queryReaderOption.ReadMethodName, value.Name);

            return propertyType != queryReaderOption.ReadMethodType ?
                CallMethodExpressionAndCast(
                    entityParameter,
                    setMethod,
                    readValueExpression,
                    propertyType) :
                CallMethodExpression(
                    entityParameter,
                    setMethod,
                    readValueExpression);
        }
        private Expression ReadValueFromReader(ParameterExpression queryReaderParameter, string readMethodName, string columnName)
        {
            MethodInfo readMethod = typeof(ISqlQueryReader).GetMethod(readMethodName);
            return Expression.Call(queryReaderParameter, readMethod, Expression.Constant(columnName, typeof(string)));
        }
    }
}
