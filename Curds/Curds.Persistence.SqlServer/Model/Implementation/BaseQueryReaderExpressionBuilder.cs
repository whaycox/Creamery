using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;

namespace Curds.Persistence.Model.Implementation
{
    using Query.Abstraction;

    internal delegate Expression PopulateValueDelegate(ParameterExpression entityParameter, PropertyInfo valueProperty, ParameterExpression queryReaderParameter);

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

        protected Expression PopulateValueFromReader(ParameterExpression entityParameter, PropertyInfo valueProperty, ParameterExpression queryReaderParameter)
        {
            if (!PopulateTypeMap.TryGetValue(valueProperty.PropertyType, out PopulateValueDelegate populateDelegate))
                throw new ArgumentException($"Unsupported type for {nameof(valueProperty)}, {valueProperty.PropertyType}");
            return populateDelegate(entityParameter, valueProperty, queryReaderParameter);
        }

        private Expression ReadValueFromReader(ParameterExpression queryReaderParameter, string readMethodName, string columnName)
        {
            MethodInfo readMethod = typeof(ISqlQueryReader).GetMethod(readMethodName);
            return Expression.Call(queryReaderParameter, readMethod, Expression.Constant(columnName, typeof(string)));
        }
        
        private Expression PopulateStringValue(ParameterExpression entityParameter, PropertyInfo valueProperty, ParameterExpression queryReaderParameter) =>
            CallMethodExpression(entityParameter, valueProperty.SetMethod, ReadValueFromReader(queryReaderParameter, nameof(ISqlQueryReader.ReadString), valueProperty.Name));
        private Expression PopulateBoolValue(ParameterExpression entityParameter, PropertyInfo valueProperty, ParameterExpression queryReaderParameter) =>
            CallMethodExpression<bool>(entityParameter, valueProperty.SetMethod, ReadValueFromReader(queryReaderParameter, nameof(ISqlQueryReader.ReadBool), valueProperty.Name));
        private Expression PopulateNullableBoolValue(ParameterExpression entityParameter, PropertyInfo valueProperty, ParameterExpression queryReaderParameter) =>
            CallMethodExpression(entityParameter, valueProperty.SetMethod, ReadValueFromReader(queryReaderParameter, nameof(ISqlQueryReader.ReadBool), valueProperty.Name));
        private Expression PopulateByteValue(ParameterExpression entityParameter, PropertyInfo valueProperty, ParameterExpression queryReaderParameter) =>
            CallMethodExpression<byte>(entityParameter, valueProperty.SetMethod, ReadValueFromReader(queryReaderParameter, nameof(ISqlQueryReader.ReadByte), valueProperty.Name));
        private Expression PopulateNullableByteValue(ParameterExpression entityParameter, PropertyInfo valueProperty, ParameterExpression queryReaderParameter) =>
            CallMethodExpression(entityParameter, valueProperty.SetMethod, ReadValueFromReader(queryReaderParameter, nameof(ISqlQueryReader.ReadByte), valueProperty.Name));
        private Expression PopulateShortValue(ParameterExpression entityParameter, PropertyInfo valueProperty, ParameterExpression queryReaderParameter) =>
            CallMethodExpression<short>(entityParameter, valueProperty.SetMethod, ReadValueFromReader(queryReaderParameter, nameof(ISqlQueryReader.ReadShort), valueProperty.Name));
        private Expression PopulateNullableShortValue(ParameterExpression entityParameter, PropertyInfo valueProperty, ParameterExpression queryReaderParameter) =>
            CallMethodExpression(entityParameter, valueProperty.SetMethod, ReadValueFromReader(queryReaderParameter, nameof(ISqlQueryReader.ReadShort), valueProperty.Name));
        private Expression PopulateIntValue(ParameterExpression entityParameter, PropertyInfo valueProperty, ParameterExpression queryReaderParameter) =>
            CallMethodExpression<int>(entityParameter, valueProperty.SetMethod, ReadValueFromReader(queryReaderParameter, nameof(ISqlQueryReader.ReadInt), valueProperty.Name));
        private Expression PopulateNullableIntValue(ParameterExpression entityParameter, PropertyInfo valueProperty, ParameterExpression queryReaderParameter) =>
            CallMethodExpression(entityParameter, valueProperty.SetMethod, ReadValueFromReader(queryReaderParameter, nameof(ISqlQueryReader.ReadInt), valueProperty.Name));
        private Expression PopulateLongValue(ParameterExpression entityParameter, PropertyInfo valueProperty, ParameterExpression queryReaderParameter) =>
            CallMethodExpression<long>(entityParameter, valueProperty.SetMethod, ReadValueFromReader(queryReaderParameter, nameof(ISqlQueryReader.ReadLong), valueProperty.Name));
        private Expression PopulateNullableLongValue(ParameterExpression entityParameter, PropertyInfo valueProperty, ParameterExpression queryReaderParameter) =>
            CallMethodExpression(entityParameter, valueProperty.SetMethod, ReadValueFromReader(queryReaderParameter, nameof(ISqlQueryReader.ReadLong), valueProperty.Name));
        private Expression PopulateDateTimeValue(ParameterExpression entityParameter, PropertyInfo valueProperty, ParameterExpression queryReaderParameter) =>
            CallMethodExpression<DateTime>(entityParameter, valueProperty.SetMethod, ReadValueFromReader(queryReaderParameter, nameof(ISqlQueryReader.ReadDateTime), valueProperty.Name));
        private Expression PopulateNullableDateTimeValue(ParameterExpression entityParameter, PropertyInfo valueProperty, ParameterExpression queryReaderParameter) =>
            CallMethodExpression(entityParameter, valueProperty.SetMethod, ReadValueFromReader(queryReaderParameter, nameof(ISqlQueryReader.ReadDateTime), valueProperty.Name));
        private Expression PopulateDateTimeOffsetValue(ParameterExpression entityParameter, PropertyInfo valueProperty, ParameterExpression queryReaderParameter) =>
            CallMethodExpression<DateTimeOffset>(entityParameter, valueProperty.SetMethod, ReadValueFromReader(queryReaderParameter, nameof(ISqlQueryReader.ReadDateTimeOffset), valueProperty.Name));
        private Expression PopulateNullableDateTimeOffsetValue(ParameterExpression entityParameter, PropertyInfo valueProperty, ParameterExpression queryReaderParameter) =>
            CallMethodExpression(entityParameter, valueProperty.SetMethod, ReadValueFromReader(queryReaderParameter, nameof(ISqlQueryReader.ReadDateTimeOffset), valueProperty.Name));
        private Expression PopulateDecimalValue(ParameterExpression entityParameter, PropertyInfo valueProperty, ParameterExpression queryReaderParameter) =>
            CallMethodExpression<decimal>(entityParameter, valueProperty.SetMethod, ReadValueFromReader(queryReaderParameter, nameof(ISqlQueryReader.ReadDecimal), valueProperty.Name));
        private Expression PopulateNullableDecimalValue(ParameterExpression entityParameter, PropertyInfo valueProperty, ParameterExpression queryReaderParameter) =>
            CallMethodExpression(entityParameter, valueProperty.SetMethod, ReadValueFromReader(queryReaderParameter, nameof(ISqlQueryReader.ReadDecimal), valueProperty.Name));
        private Expression PopulateDoubleValue(ParameterExpression entityParameter, PropertyInfo valueProperty, ParameterExpression queryReaderParameter) =>
            CallMethodExpression<double>(entityParameter, valueProperty.SetMethod, ReadValueFromReader(queryReaderParameter, nameof(ISqlQueryReader.ReadDouble), valueProperty.Name));
        private Expression PopulateNullableDoubleValue(ParameterExpression entityParameter, PropertyInfo valueProperty, ParameterExpression queryReaderParameter) =>
            CallMethodExpression(entityParameter, valueProperty.SetMethod, ReadValueFromReader(queryReaderParameter, nameof(ISqlQueryReader.ReadDouble), valueProperty.Name));
    }
}
