using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;

namespace Curds.Persistence.Model.Implementation
{
    using Query.Domain;
    using Abstraction;
    using Domain;

    internal class ValueExpressionBuilder : IValueExpressionBuilder
    {

        private Dictionary<Type, Type> ValueTypeMap { get; } = new Dictionary<Type, Type>
        {
            { typeof(string), typeof(StringValue) },
            { typeof(bool), typeof(BoolValue) },
            { typeof(int), typeof(IntValue) },
            { typeof(DateTime), typeof(DateTimeValue) },
            { typeof(DateTimeOffset), typeof(DateTimeOffsetValue) },
            { typeof(decimal), typeof(DecimalValue) },
            { typeof(double), typeof(DoubleValue) },
        };

        private Dictionary<Type, AssignValueDelegate> ValueAssignDelegateMap { get; }

        public ValueExpressionBuilder()
        {
            ValueAssignDelegateMap = new Dictionary<Type, AssignValueDelegate>
            {
                { typeof(StringValue), AssignStringValue },
                { typeof(BoolValue), AssignBoolValue },
                { typeof(IntValue), AssignIntValue },
                { typeof(DateTimeValue), AssignDateTimeValue },
                { typeof(DateTimeOffsetValue), AssignDateTimeOffsetValue },
                { typeof(DecimalValue), AssignDecimalValue },
                { typeof(DoubleValue), AssignDoubleValue },
            };
        }

        private Expression AssignStringValue(ParameterExpression valueParameter, PropertyInfo valueProperty, ParameterExpression entityParameter)
        {
            MethodInfo getEntityValueMethod = valueProperty.GetMethod;
            MethodInfo setStringValueMethod = typeof(StringValue)
                .GetProperty(nameof(StringValue.String))
                .SetMethod;

            return Expression.Call(
                valueParameter,
                setStringValueMethod,
                Expression.Call(entityParameter, getEntityValueMethod));
        }
        private Expression AssignBoolValue(ParameterExpression valueParameter, PropertyInfo valueProperty, ParameterExpression entityParameter)
        {
            MethodInfo getEntityValueMethod = valueProperty.GetMethod;
            MethodInfo setBoolValueMethod = typeof(BoolValue)
                .GetProperty(nameof(BoolValue.Bool))
                .SetMethod;

            return Expression.Call(
                valueParameter,
                setBoolValueMethod,
                Expression.Convert(
                    Expression.Call(entityParameter, getEntityValueMethod),
                    typeof(bool?)));
        }
        private Expression AssignIntValue(ParameterExpression valueParameter, PropertyInfo valueProperty, ParameterExpression entityParameter)
        {
            MethodInfo getEntityValueMethod = valueProperty.GetMethod;
            MethodInfo setIntValueMethod = typeof(IntValue)
                .GetProperty(nameof(IntValue.Int))
                .SetMethod;

            return Expression.Call(
                valueParameter,
                setIntValueMethod,
                Expression.Convert(
                    Expression.Call(entityParameter, getEntityValueMethod),
                    typeof(int?)));
        }
        private Expression AssignDateTimeValue(ParameterExpression valueParameter, PropertyInfo valueProperty, ParameterExpression entityParameter)
        {
            MethodInfo getEntityValueMethod = valueProperty.GetMethod;
            MethodInfo setDateTimeValueMethod = typeof(DateTimeValue)
                .GetProperty(nameof(DateTimeValue.DateTime))
                .SetMethod;

            return Expression.Call(
                valueParameter,
                setDateTimeValueMethod,
                Expression.Convert(
                    Expression.Call(entityParameter, getEntityValueMethod),
                    typeof(DateTime?)));
        }
        private Expression AssignDateTimeOffsetValue(ParameterExpression valueParameter, PropertyInfo valueProperty, ParameterExpression entityParameter)
        {
            MethodInfo getEntityValueMethod = valueProperty.GetMethod;
            MethodInfo setDateTimeOffsetValueMethod = typeof(DateTimeOffsetValue)
                .GetProperty(nameof(DateTimeOffsetValue.DateTimeOffset))
                .SetMethod;

            return Expression.Call(
                valueParameter,
                setDateTimeOffsetValueMethod,
                Expression.Convert(
                    Expression.Call(entityParameter, getEntityValueMethod),
                    typeof(DateTimeOffset?)));
        }
        private Expression AssignDecimalValue(ParameterExpression valueParameter, PropertyInfo valueProperty, ParameterExpression entityParameter)
        {
            MethodInfo getEntityValueMethod = valueProperty.GetMethod;
            MethodInfo setDecimalValueMethod = typeof(DecimalValue)
                .GetProperty(nameof(DecimalValue.Decimal))
                .SetMethod;

            return Expression.Call(
                valueParameter,
                setDecimalValueMethod,
                Expression.Convert(
                    Expression.Call(entityParameter, getEntityValueMethod),
                    typeof(decimal?)));
        }
        private Expression AssignDoubleValue(ParameterExpression valueParameter, PropertyInfo valueProperty, ParameterExpression entityParameter)
        {
            MethodInfo getEntityValueMethod = valueProperty.GetMethod;
            MethodInfo setDoubleValueMethod = typeof(DoubleValue)
                .GetProperty(nameof(DoubleValue.Double))
                .SetMethod;

            return Expression.Call(
                valueParameter,
                setDoubleValueMethod,
                Expression.Convert(
                    Expression.Call(entityParameter, getEntityValueMethod),
                    typeof(double?)));
        }

        public Type ValueType(Type propertyType)
        {
            if (!ValueTypeMap.TryGetValue(propertyType, out Type valueType))
                throw new ModelException($"{propertyType.FullName} is an unsupported entity value type");
            return valueType;
        }

        public AssignValueDelegate AssignValue(Type valueType)
        {
            if (!ValueAssignDelegateMap.TryGetValue(valueType, out AssignValueDelegate assignValueDelegate))
                throw new ModelException($"Unsupported value type {valueType.FullName}");
            return assignValueDelegate;
        }
    }
}
