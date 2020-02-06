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
        private IValueExpressionBuilder ExpressionBuilder { get; }
        private ITypeMapper TypeMapper { get; }
        private IModelConfigurationFactory ConfigurationFactory { get; }

        public DelegateMapper(
            IValueExpressionBuilder expressionBuilder,
            ITypeMapper typeMapper,
            IModelConfigurationFactory configurationFactory)
        {

            ExpressionBuilder = expressionBuilder;
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
            Type valueType = ExpressionBuilder.ValueType(valueProperty.PropertyType);
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
            AssignValueDelegate assignValueDelegate = ExpressionBuilder.AssignValue(valueType);
            List<Expression> addValueBlockExpressions = new List<Expression>
            {
                Expression.Assign(valueParameter, Expression.New(valueConstructor)),
                assignNameExpression,
                assignValueDelegate(valueParameter, valueProperty, entityParameter),
                addValueExpression,
            };

            return Expression.Block(addValueBlockParameters, addValueBlockExpressions);
        }
    }
}
