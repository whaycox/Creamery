using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Curds.Persistence.Model.Implementation
{
    using Abstraction;
    using Configuration.Abstraction;
    using Domain;
    using Persistence.Abstraction;
    using Persistence.Domain;
    using Query.Domain;

    internal class DelegateMapper : IDelegateMapper
    {
        private delegate Expression AssignValueDelegate(ParameterExpression valueParameter, PropertyInfo valueProperty, ParameterExpression entityParameter);

        private Dictionary<Type, Type> ValueTypeMap { get; } = new Dictionary<Type, Type>
        {
            { typeof(string), typeof(StringValue) },
            { typeof(int), typeof(IntValue) },
        };
        private Dictionary<Type, AssignValueDelegate> ValueAssignDelegateMap { get; }

        private ITypeMapper TypeMapper { get; }
        private IModelConfigurationFactory ConfigurationFactory { get; }

        public DelegateMapper(
            ITypeMapper typeMapper,
            IModelConfigurationFactory configurationFactory)
        {
            ValueAssignDelegateMap = new Dictionary<Type, AssignValueDelegate>
            {
                { typeof(StringValue), AssignStringValue },
                { typeof(IntValue), AssignIntValue },
            };

            TypeMapper = typeMapper;
            ConfigurationFactory = configurationFactory;
        }

        public ValueEntityDelegate MapValueEntityDelegate<TModel>(Type entityType)
            where TModel : IDataModel
        {
            ParameterExpression baseEntityParameter = Expression.Parameter(typeof(BaseEntity), nameof(baseEntityParameter));
            ParameterExpression entityParameter = Expression.Parameter(entityType, nameof(entityParameter));
            ParameterExpression valueEntityParameter = Expression.Parameter(typeof(ValueEntity), nameof(valueEntityParameter));
            List<ParameterExpression> builderExpressionParameters = new List<ParameterExpression>
            {
                entityParameter,
                valueEntityParameter,
            };

            ConstructorInfo valueEntityConstructor = typeof(ValueEntity).GetConstructor(new Type[0]);
            LabelTarget returnLabel = Expression.Label(typeof(ValueEntity));
            List<Expression> builderExpressions = new List<Expression>
            {
                Expression.Assign(entityParameter, Expression.Convert(baseEntityParameter, entityType)),
                Expression.Assign(valueEntityParameter, Expression.New(valueEntityConstructor)),
            };

            IModelEntityConfiguration entityConfiguration = ConfigurationFactory.Build<TModel>(entityType);
            foreach (PropertyInfo valueInfo in TypeMapper.ValueTypes(entityType))
                if (valueInfo.Name != entityConfiguration.Identity)
                    builderExpressions.Add(AddValueExpression(valueInfo, entityParameter, valueEntityParameter));

            builderExpressions.AddRange(new List<Expression>
            {
                Expression.Return(returnLabel, valueEntityParameter),
                Expression.Label(returnLabel, valueEntityParameter),
            });

            BlockExpression valueEntityBlock = Expression.Block(builderExpressionParameters, builderExpressions);

            return Expression
                .Lambda<ValueEntityDelegate>(valueEntityBlock, baseEntityParameter)
                .Compile();
        }
        private Expression AddValueExpression(PropertyInfo valueProperty, ParameterExpression entityParameter, ParameterExpression valueEntityParameter)
        {
            if (!ValueTypeMap.TryGetValue(valueProperty.PropertyType, out Type valueType))
                throw new ModelException($"{valueProperty.PropertyType.FullName} is an unsupported entity value type");

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
            List<Expression> addValueBlockExpressions = new List<Expression>
            {
                Expression.Assign(valueParameter, Expression.New(valueConstructor)),
                assignNameExpression,
                CreateAssignValueExpression(
                    valueType,
                    valueParameter,
                    valueProperty,
                    entityParameter),
                addValueExpression,
            };

            return Expression.Block(addValueBlockParameters, addValueBlockExpressions);
        }
        private Expression CreateAssignValueExpression(Type valueType, ParameterExpression valueParameter, PropertyInfo valueProperty, ParameterExpression entityParameter)
        {
            if (!ValueAssignDelegateMap.TryGetValue(valueType, out AssignValueDelegate assignValueDelegate))
                throw new ModelException($"Unsupported value type {valueType.FullName}");

            return assignValueDelegate(valueParameter, valueProperty, entityParameter);
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
    }
}
