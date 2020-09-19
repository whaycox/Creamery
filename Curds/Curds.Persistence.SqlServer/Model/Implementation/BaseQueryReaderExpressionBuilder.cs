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
        private static IReadOnlyDictionary<SqlDbType, string> PopulateTypeMap { get; } = new Dictionary<SqlDbType, string>
        {
            { SqlDbType.NVarChar, nameof(ISqlQueryReader.ReadString) },
            { SqlDbType.Bit, nameof(ISqlQueryReader.ReadBool) },
            { SqlDbType.TinyInt, nameof(ISqlQueryReader.ReadByte) },
            { SqlDbType.SmallInt, nameof(ISqlQueryReader.ReadShort) },
            { SqlDbType.Int, nameof(ISqlQueryReader.ReadInt) },
            { SqlDbType.BigInt, nameof(ISqlQueryReader.ReadLong) },
            { SqlDbType.DateTime, nameof(ISqlQueryReader.ReadDateTime) },
            { SqlDbType.DateTimeOffset, nameof(ISqlQueryReader.ReadDateTimeOffset) },
            { SqlDbType.Decimal, nameof(ISqlQueryReader.ReadDecimal) },
            { SqlDbType.Float, nameof(ISqlQueryReader.ReadDouble) },
        };
        private static IReadOnlyDictionary<SqlDbType, Type> ReadTypeMap { get; } = new Dictionary<SqlDbType, Type>
        {
            { SqlDbType.NVarChar, typeof(string) },
            { SqlDbType.Bit, typeof(bool?) },
            { SqlDbType.TinyInt, typeof(byte?) },
            { SqlDbType.SmallInt, typeof(short?) },
            { SqlDbType.Int, typeof(int?) },
            { SqlDbType.BigInt, typeof(long?) },
            { SqlDbType.DateTime, typeof(DateTime?) },
            { SqlDbType.DateTimeOffset, typeof(DateTimeOffset?) },
            { SqlDbType.Decimal, typeof(decimal?) },
            { SqlDbType.Float, typeof(double?) },
        };

        protected Expression PopulateValueFromReader(ParameterExpression entityParameter, IValueModel value, ParameterExpression queryReaderParameter)
        {
            if (!PopulateTypeMap.TryGetValue(value.SqlType, out string readMethodName) ||
                !ReadTypeMap.TryGetValue(value.SqlType, out Type readAsType))
                    throw new ModelException($"Unsupported type for {value.Name}, {value.SqlType}");

            PropertyInfo valueProperty = value.Property;
            Type propertyType = valueProperty.PropertyType;
            MethodInfo setMethod = valueProperty.SetMethod;
            Expression readValueExpression = ReadValueFromReader(queryReaderParameter, readMethodName, value.Name);

            return propertyType != readAsType ?
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
