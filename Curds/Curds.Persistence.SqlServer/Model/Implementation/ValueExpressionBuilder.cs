using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Curds.Persistence.Model.Implementation
{
    using Abstraction;
    using Domain;
    using Persistence.Domain;
    using Query.Domain;
    using Query.Values.Domain;
    using Persistence.Abstraction;

    internal delegate Expression AssignValueDelegate(ParameterExpression valueParameter, PropertyInfo entityProperty, ParameterExpression entityParameter);
    internal delegate IEnumerable<Expression> AddValueExpressionsDelegate(ParameterExpression entityParameter, ParameterExpression valueEntityParameter);

    internal class ValueExpressionBuilder : BaseExpressionBuilder, IValueExpressionBuilder
    {
        private Dictionary<Type, Type> ValueTypeMap { get; } = new Dictionary<Type, Type>
        {
            { typeof(string), typeof(StringValue) },
            { typeof(bool), typeof(BoolValue) },
            { typeof(bool?), typeof(NullableBoolValue) },
            { typeof(byte), typeof(ByteValue) },
            { typeof(byte?), typeof(NullableByteValue) },
            { typeof(short), typeof(ShortValue) },
            { typeof(short?), typeof(NullableShortValue) },
            { typeof(int), typeof(IntValue) },
            { typeof(int?), typeof(NullableIntValue) },
            { typeof(long), typeof(LongValue) },
            { typeof(long?), typeof(NullableLongValue) },
            { typeof(DateTime), typeof(DateTimeValue) },
            { typeof(DateTime?), typeof(NullableDateTimeValue) },
            { typeof(DateTimeOffset), typeof(DateTimeOffsetValue) },
            { typeof(DateTimeOffset?), typeof(NullableDateTimeOffsetValue) },
            { typeof(decimal), typeof(DecimalValue) },
            { typeof(decimal?), typeof(NullableDecimalValue) },
            { typeof(double), typeof(DoubleValue) },
            { typeof(double?), typeof(NullableDoubleValue) },
        };

        private Dictionary<Type, AssignValueDelegate> ValueAssignDelegateMap { get; }

        public ValueExpressionBuilder()
        {
            ValueAssignDelegateMap = new Dictionary<Type, AssignValueDelegate>
            {
                { typeof(StringValue), AssignStringValue },
                { typeof(BoolValue), AssignBoolValue },
                { typeof(NullableBoolValue), AssignNullableBoolValue },
                { typeof(ByteValue), AssignByteValue },
                { typeof(NullableByteValue), AssignNullableByteValue },
                { typeof(ShortValue), AssignShortValue },
                { typeof(NullableShortValue), AssignNullableShortValue },
                { typeof(IntValue), AssignIntValue },
                { typeof(NullableIntValue), AssignNullableIntValue },
                { typeof(LongValue), AssignLongValue },
                { typeof(NullableLongValue), AssignNullableLongValue },
                { typeof(DateTimeValue), AssignDateTimeValue },
                { typeof(NullableDateTimeValue), AssignNullableDateTimeValue },
                { typeof(DateTimeOffsetValue), AssignDateTimeOffsetValue },
                { typeof(NullableDateTimeOffsetValue), AssignNullableDateTimeOffsetValue },
                { typeof(DecimalValue), AssignDecimalValue },
                { typeof(NullableDecimalValue), AssignNullableDecimalValue },
                { typeof(DoubleValue), AssignDoubleValue },
                { typeof(NullableDoubleValue), AssignNullableDoubleValue },
            };
        }

        public ValueEntityDelegate BuildValueEntityDelegate(IEntityModel entityModel)
        {
            ParameterExpression iEntityParameter = Expression.Parameter(typeof(IEntity), nameof(iEntityParameter));
            ParameterExpression entityParameter = Expression.Parameter(entityModel.EntityType, nameof(entityParameter));
            ParameterExpression valueEntityParameter = Expression.Parameter(typeof(ValueEntity), nameof(valueEntityParameter));
            List<ParameterExpression> builderExpressionParameters = new List<ParameterExpression>
            {
                entityParameter,
                valueEntityParameter,
            };

            ConstructorInfo valueEntityConstructor = typeof(ValueEntity).GetConstructor(new Type[0]);
            List<Expression> builderExpressions = new List<Expression>
            {
                Expression.Assign(entityParameter, Expression.Convert(iEntityParameter, entityModel.EntityType)),
                Expression.Assign(valueEntityParameter, Expression.New(valueEntityConstructor)),
            };

            foreach (IValueModel value in entityModel.NonIdentities)
                builderExpressions.Add(AddValueExpression(value.Property, entityParameter, valueEntityParameter));

            LabelTarget returnLabel = Expression.Label(typeof(ValueEntity));
            builderExpressions.Add(Expression.Return(returnLabel, valueEntityParameter));
            builderExpressions.Add(Expression.Label(returnLabel, valueEntityParameter));

            BlockExpression valueEntityBlock = Expression.Block(builderExpressionParameters, builderExpressions);

            return Expression
                .Lambda<ValueEntityDelegate>(valueEntityBlock, iEntityParameter)
                .Compile();
        }

        public Expression AddValueExpression(PropertyInfo valueProperty, ParameterExpression entityParameter, ParameterExpression valueEntityParameter)
        {
            Type valueType = ValueType(valueProperty.PropertyType);
            ParameterExpression valueParameter = Expression.Parameter(valueType, nameof(valueParameter));
            List<ParameterExpression> addValueBlockParameters = new List<ParameterExpression>
            {
                valueParameter,
            };

            PropertyInfo valueNameProperty = typeof(Value).GetProperty(nameof(Value.Name));
            MethodInfo setValueNameMethod = valueNameProperty.SetMethod;
            Expression assignNameExpression = Expression.Call(valueParameter, setValueNameMethod, Expression.Constant(valueProperty.Name, typeof(string)));

            PropertyInfo valuesCollectionProperty = typeof(ValueEntity).GetProperty(nameof(ValueEntity.Values));
            MethodInfo getValuesCollectionMethod = valuesCollectionProperty.GetMethod;
            MethodInfo addValueMethod = valuesCollectionProperty.PropertyType.GetMethod(nameof(List<Value>.Add));
            Expression addValueExpression = Expression.Call(
                Expression.Call(valueEntityParameter, getValuesCollectionMethod),
                addValueMethod,
                valueParameter);

            ConstructorInfo valueConstructor = valueType.GetConstructor(new Type[0]);
            AssignValueDelegate assignValueDelegate = AssignValue(valueType);
            List<Expression> addValueBlockExpressions = new List<Expression>
            {
                Expression.Assign(valueParameter, Expression.New(valueConstructor)),
                assignNameExpression,
                assignValueDelegate(valueParameter, valueProperty, entityParameter),
                addValueExpression,
            };

            return Expression.Block(addValueBlockParameters, addValueBlockExpressions);
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

        #region AssignValueMethods
        private Expression GetEntityPropertyExpression(PropertyInfo entityProperty, ParameterExpression entityParameter) =>
            Expression.Call(entityParameter, entityProperty.GetMethod);

        private MethodInfo SetStringValueMethod => typeof(StringValue).GetProperty(nameof(StringValue.String)).SetMethod;
        private Expression AssignStringValue(ParameterExpression valueParameter, PropertyInfo entityProperty, ParameterExpression entityParameter) =>
            CallMethodExpression(valueParameter, SetStringValueMethod, GetEntityPropertyExpression(entityProperty, entityParameter));

        private MethodInfo SetBoolValueMethod = typeof(BoolValue).GetProperty(nameof(BoolValue.Bool)).SetMethod;
        private Expression AssignBoolValue(ParameterExpression valueParameter, PropertyInfo entityProperty, ParameterExpression entityParameter) =>
            CallMethodExpression<bool?>(valueParameter, SetBoolValueMethod, GetEntityPropertyExpression(entityProperty, entityParameter));
        private Expression AssignNullableBoolValue(ParameterExpression valueParameter, PropertyInfo entityProperty, ParameterExpression entityParameter) =>
            CallMethodExpression(valueParameter, SetBoolValueMethod, GetEntityPropertyExpression(entityProperty, entityParameter));

        private MethodInfo SetByteValueMethod = typeof(ByteValue).GetProperty(nameof(ByteValue.Byte)).SetMethod;
        private Expression AssignByteValue(ParameterExpression valueParameter, PropertyInfo entityProperty, ParameterExpression entityParameter) =>
            CallMethodExpression<byte?>(valueParameter, SetByteValueMethod, GetEntityPropertyExpression(entityProperty, entityParameter));
        private Expression AssignNullableByteValue(ParameterExpression valueParameter, PropertyInfo entityProperty, ParameterExpression entityParameter) =>
            CallMethodExpression(valueParameter, SetByteValueMethod, GetEntityPropertyExpression(entityProperty, entityParameter));

        private MethodInfo SetShortValueMethod = typeof(ShortValue).GetProperty(nameof(ShortValue.Short)).SetMethod;
        private Expression AssignShortValue(ParameterExpression valueParameter, PropertyInfo entityProperty, ParameterExpression entityParameter) =>
            CallMethodExpression<short?>(valueParameter, SetShortValueMethod, GetEntityPropertyExpression(entityProperty, entityParameter));
        private Expression AssignNullableShortValue(ParameterExpression valueParameter, PropertyInfo entityProperty, ParameterExpression entityParameter) =>
            CallMethodExpression(valueParameter, SetShortValueMethod, GetEntityPropertyExpression(entityProperty, entityParameter));

        private MethodInfo SetIntValueMethod = typeof(IntValue).GetProperty(nameof(IntValue.Int)).SetMethod;
        private Expression AssignIntValue(ParameterExpression valueParameter, PropertyInfo entityProperty, ParameterExpression entityParameter) =>
            CallMethodExpression<int?>(valueParameter, SetIntValueMethod, GetEntityPropertyExpression(entityProperty, entityParameter));
        private Expression AssignNullableIntValue(ParameterExpression valueParameter, PropertyInfo entityProperty, ParameterExpression entityParameter) =>
            CallMethodExpression(valueParameter, SetIntValueMethod, GetEntityPropertyExpression(entityProperty, entityParameter));

        private MethodInfo SetLongValueMethod = typeof(LongValue).GetProperty(nameof(LongValue.Long)).SetMethod;
        private Expression AssignLongValue(ParameterExpression valueParameter, PropertyInfo entityProperty, ParameterExpression entityParameter) =>
            CallMethodExpression<long?>(valueParameter, SetLongValueMethod, GetEntityPropertyExpression(entityProperty, entityParameter));
        private Expression AssignNullableLongValue(ParameterExpression valueParameter, PropertyInfo entityProperty, ParameterExpression entityParameter) =>
            CallMethodExpression(valueParameter, SetLongValueMethod, GetEntityPropertyExpression(entityProperty, entityParameter));

        private MethodInfo SetDateTimeValueMethod = typeof(DateTimeValue).GetProperty(nameof(DateTimeValue.DateTime)).SetMethod;
        private Expression AssignDateTimeValue(ParameterExpression valueParameter, PropertyInfo entityProperty, ParameterExpression entityParameter) =>
            CallMethodExpression<DateTime?>(valueParameter, SetDateTimeValueMethod, GetEntityPropertyExpression(entityProperty, entityParameter));
        private Expression AssignNullableDateTimeValue(ParameterExpression valueParameter, PropertyInfo entityProperty, ParameterExpression entityParameter) =>
            CallMethodExpression(valueParameter, SetDateTimeValueMethod, GetEntityPropertyExpression(entityProperty, entityParameter));

        private MethodInfo SetDateTimeOffsetValueMethod = typeof(DateTimeOffsetValue).GetProperty(nameof(DateTimeOffsetValue.DateTimeOffset)).SetMethod;
        private Expression AssignDateTimeOffsetValue(ParameterExpression valueParameter, PropertyInfo entityProperty, ParameterExpression entityParameter) =>
            CallMethodExpression<DateTimeOffset?>(valueParameter, SetDateTimeOffsetValueMethod, GetEntityPropertyExpression(entityProperty, entityParameter));
        private Expression AssignNullableDateTimeOffsetValue(ParameterExpression valueParameter, PropertyInfo entityProperty, ParameterExpression entityParameter) =>
            CallMethodExpression(valueParameter, SetDateTimeOffsetValueMethod, GetEntityPropertyExpression(entityProperty, entityParameter));

        private MethodInfo SetDecimalValueMethod = typeof(DecimalValue).GetProperty(nameof(DecimalValue.Decimal)).SetMethod;
        private Expression AssignDecimalValue(ParameterExpression valueParameter, PropertyInfo entityProperty, ParameterExpression entityParameter) =>
            CallMethodExpression<decimal?>(valueParameter, SetDecimalValueMethod, GetEntityPropertyExpression(entityProperty, entityParameter));
        private Expression AssignNullableDecimalValue(ParameterExpression valueParameter, PropertyInfo entityProperty, ParameterExpression entityParameter) =>
            CallMethodExpression(valueParameter, SetDecimalValueMethod, GetEntityPropertyExpression(entityProperty, entityParameter));

        private MethodInfo SetDoubleValueMethod = typeof(DoubleValue).GetProperty(nameof(DoubleValue.Double)).SetMethod;
        private Expression AssignDoubleValue(ParameterExpression valueParameter, PropertyInfo entityProperty, ParameterExpression entityParameter) =>
            CallMethodExpression<double?>(valueParameter, SetDoubleValueMethod, GetEntityPropertyExpression(entityProperty, entityParameter));
        private Expression AssignNullableDoubleValue(ParameterExpression valueParameter, PropertyInfo entityProperty, ParameterExpression entityParameter) =>
            CallMethodExpression(valueParameter, SetDoubleValueMethod, GetEntityPropertyExpression(entityProperty, entityParameter));
        #endregion
    }
}
