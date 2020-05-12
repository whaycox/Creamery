using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;

namespace Curds.Persistence.Model.Implementation
{
    using Query.Abstraction;
    using Abstraction;
    using Domain;

    internal delegate Expression PopulateValueDelegate(ParameterExpression entityParameter, IValueModel value, ParameterExpression queryReaderParameter);

    internal abstract class BaseQueryReaderExpressionBuilder : BaseExpressionBuilder
    {
        private Dictionary<Type, PopulateValueDelegate> PopulateTypeMap { get; }

        public BaseQueryReaderExpressionBuilder()
        {
            PopulateTypeMap = new Dictionary<Type, PopulateValueDelegate>
            {
                { typeof(string), PopulateStringValue },
                { typeof(bool), PopulateBoolValue },
                { typeof(bool?), PopulateNullableBoolValue },
                { typeof(byte), PopulateByteValue },
                { typeof(byte?), PopulateNullableByteValue },
                { typeof(short), PopulateShortValue },
                { typeof(short?), PopulateNullableShortValue },
                { typeof(int), PopulateIntValue },
                { typeof(int?), PopulateNullableIntValue },
                { typeof(long), PopulateLongValue },
                { typeof(long?), PopulateNullableLongValue },
                { typeof(DateTime), PopulateDateTimeValue },
                { typeof(DateTime?), PopulateNullableDateTimeValue },
                { typeof(DateTimeOffset), PopulateDateTimeOffsetValue },
                { typeof(DateTimeOffset?), PopulateNullableDateTimeOffsetValue },
                { typeof(decimal), PopulateDecimalValue },
                { typeof(decimal?), PopulateNullableDecimalValue },
                { typeof(double), PopulateDoubleValue },
                { typeof(double?), PopulateNullableDoubleValue }
            };
        }

        protected Expression PopulateValueFromReader(ParameterExpression entityParameter, IValueModel value, ParameterExpression queryReaderParameter)
        {
            if (!PopulateTypeMap.TryGetValue(value.Property.PropertyType, out PopulateValueDelegate populateDelegate))
                throw new ModelException($"Unsupported type for {value.Name}, {value.Property.PropertyType}");
            return populateDelegate(entityParameter, value, queryReaderParameter);
        }

        private Expression ReadValueFromReader(ParameterExpression queryReaderParameter, string readMethodName, string columnName)
        {
            MethodInfo readMethod = typeof(ISqlQueryReader).GetMethod(readMethodName);
            return Expression.Call(queryReaderParameter, readMethod, Expression.Constant(columnName, typeof(string)));
        }
        
        private Expression PopulateStringValue(ParameterExpression entityParameter, IValueModel value, ParameterExpression queryReaderParameter) =>
            CallMethodExpression(entityParameter, value.Property.SetMethod, ReadValueFromReader(queryReaderParameter, nameof(ISqlQueryReader.ReadString), value.Name));
        private Expression PopulateBoolValue(ParameterExpression entityParameter, IValueModel value, ParameterExpression queryReaderParameter) =>
            CallMethodExpression<bool>(entityParameter, value.Property.SetMethod, ReadValueFromReader(queryReaderParameter, nameof(ISqlQueryReader.ReadBool), value.Name));
        private Expression PopulateNullableBoolValue(ParameterExpression entityParameter, IValueModel value, ParameterExpression queryReaderParameter) =>
            CallMethodExpression(entityParameter, value.Property.SetMethod, ReadValueFromReader(queryReaderParameter, nameof(ISqlQueryReader.ReadBool), value.Name));
        private Expression PopulateByteValue(ParameterExpression entityParameter, IValueModel value, ParameterExpression queryReaderParameter) =>
            CallMethodExpression<byte>(entityParameter, value.Property.SetMethod, ReadValueFromReader(queryReaderParameter, nameof(ISqlQueryReader.ReadByte), value.Name));
        private Expression PopulateNullableByteValue(ParameterExpression entityParameter, IValueModel value, ParameterExpression queryReaderParameter) =>
            CallMethodExpression(entityParameter, value.Property.SetMethod, ReadValueFromReader(queryReaderParameter, nameof(ISqlQueryReader.ReadByte), value.Name));
        private Expression PopulateShortValue(ParameterExpression entityParameter, IValueModel value, ParameterExpression queryReaderParameter) =>
            CallMethodExpression<short>(entityParameter, value.Property.SetMethod, ReadValueFromReader(queryReaderParameter, nameof(ISqlQueryReader.ReadShort), value.Name));
        private Expression PopulateNullableShortValue(ParameterExpression entityParameter, IValueModel value, ParameterExpression queryReaderParameter) =>
            CallMethodExpression(entityParameter, value.Property.SetMethod, ReadValueFromReader(queryReaderParameter, nameof(ISqlQueryReader.ReadShort), value.Name));
        private Expression PopulateIntValue(ParameterExpression entityParameter, IValueModel value, ParameterExpression queryReaderParameter) =>
            CallMethodExpression<int>(entityParameter, value.Property.SetMethod, ReadValueFromReader(queryReaderParameter, nameof(ISqlQueryReader.ReadInt), value.Name));
        private Expression PopulateNullableIntValue(ParameterExpression entityParameter, IValueModel value, ParameterExpression queryReaderParameter) =>
            CallMethodExpression(entityParameter, value.Property.SetMethod, ReadValueFromReader(queryReaderParameter, nameof(ISqlQueryReader.ReadInt), value.Name));
        private Expression PopulateLongValue(ParameterExpression entityParameter, IValueModel value, ParameterExpression queryReaderParameter) =>
            CallMethodExpression<long>(entityParameter, value.Property.SetMethod, ReadValueFromReader(queryReaderParameter, nameof(ISqlQueryReader.ReadLong), value.Name));
        private Expression PopulateNullableLongValue(ParameterExpression entityParameter, IValueModel value, ParameterExpression queryReaderParameter) =>
            CallMethodExpression(entityParameter, value.Property.SetMethod, ReadValueFromReader(queryReaderParameter, nameof(ISqlQueryReader.ReadLong), value.Name));
        private Expression PopulateDateTimeValue(ParameterExpression entityParameter, IValueModel value, ParameterExpression queryReaderParameter) =>
            CallMethodExpression<DateTime>(entityParameter, value.Property.SetMethod, ReadValueFromReader(queryReaderParameter, nameof(ISqlQueryReader.ReadDateTime), value.Name));
        private Expression PopulateNullableDateTimeValue(ParameterExpression entityParameter, IValueModel value, ParameterExpression queryReaderParameter) =>
            CallMethodExpression(entityParameter, value.Property.SetMethod, ReadValueFromReader(queryReaderParameter, nameof(ISqlQueryReader.ReadDateTime), value.Name));
        private Expression PopulateDateTimeOffsetValue(ParameterExpression entityParameter, IValueModel value, ParameterExpression queryReaderParameter) =>
            CallMethodExpression<DateTimeOffset>(entityParameter, value.Property.SetMethod, ReadValueFromReader(queryReaderParameter, nameof(ISqlQueryReader.ReadDateTimeOffset), value.Name));
        private Expression PopulateNullableDateTimeOffsetValue(ParameterExpression entityParameter, IValueModel value, ParameterExpression queryReaderParameter) =>
            CallMethodExpression(entityParameter, value.Property.SetMethod, ReadValueFromReader(queryReaderParameter, nameof(ISqlQueryReader.ReadDateTimeOffset), value.Name));
        private Expression PopulateDecimalValue(ParameterExpression entityParameter, IValueModel value, ParameterExpression queryReaderParameter) =>
            CallMethodExpression<decimal>(entityParameter, value.Property.SetMethod, ReadValueFromReader(queryReaderParameter, nameof(ISqlQueryReader.ReadDecimal), value.Name));
        private Expression PopulateNullableDecimalValue(ParameterExpression entityParameter, IValueModel value, ParameterExpression queryReaderParameter) =>
            CallMethodExpression(entityParameter, value.Property.SetMethod, ReadValueFromReader(queryReaderParameter, nameof(ISqlQueryReader.ReadDecimal), value.Name));
        private Expression PopulateDoubleValue(ParameterExpression entityParameter, IValueModel value, ParameterExpression queryReaderParameter) =>
            CallMethodExpression<double>(entityParameter, value.Property.SetMethod, ReadValueFromReader(queryReaderParameter, nameof(ISqlQueryReader.ReadDouble), value.Name));
        private Expression PopulateNullableDoubleValue(ParameterExpression entityParameter, IValueModel value, ParameterExpression queryReaderParameter) =>
            CallMethodExpression(entityParameter, value.Property.SetMethod, ReadValueFromReader(queryReaderParameter, nameof(ISqlQueryReader.ReadDouble), value.Name));
    }
}
