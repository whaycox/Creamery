using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;

namespace Curds.Persistence.Query.Implementation
{
    using Abstraction;
    using Persistence.Abstraction;
    using Domain;
    using Persistence.Domain;

    internal class ModelMapper : IModelMapper
    {
        private const string InvalidModelPropertiesMessage = "All model properties must be ITable<SomeEntity>";

        private delegate Expression AssignValueDelegate(ParameterExpression valueParameter, PropertyInfo valueProperty, ParameterExpression entityParameter);

        private Dictionary<Type, Type> ValueTypeMap { get; } = new Dictionary<Type, Type>
        {
            { typeof(string), typeof(StringValue) },
            { typeof(int), typeof(IntValue) },
        };
        private Dictionary<Type, AssignValueDelegate> ValueAssignDelegateMap { get; }

        public ModelMapper()
        {
            ValueAssignDelegateMap = new Dictionary<Type, AssignValueDelegate>
            {
                { typeof(StringValue), AssignStringValue },
                { typeof(IntValue), AssignIntValue },
            };
        }

        private Type ParseModelPropertyToTableType(PropertyInfo propertyInfo)
        {
            Type propertyType = propertyInfo.PropertyType;
            if (!propertyType.IsGenericType)
                throw new ModelException(InvalidModelPropertiesMessage);
            Type genericType = propertyType.GetGenericTypeDefinition();
            if (genericType != typeof(ITable<>))
                throw new ModelException(InvalidModelPropertiesMessage);

            return propertyType;
        }
        private string ExtractTableName(PropertyInfo propertyInfo) => propertyInfo.Name;
        private Type ExtractEntityType(Type tableType) => tableType.GenericTypeArguments[0];
        private Table BuildTable(Type entityType, string tableName)
        {
            Table table = new Table { Name = tableName };
            foreach (PropertyInfo propertyInfo in ValuePropertiesForEntityType(entityType))
                table.Columns.Add(BuildColumn(propertyInfo));
            return table;
        }
        private Column BuildColumn(PropertyInfo propertyInfo) => new Column
        {
            Name = propertyInfo.Name,
        };

        private IEnumerable<PropertyInfo> ValuePropertiesForEntityType(Type entityType) => entityType
            .GetProperties()
            .Where(property => property.CanRead && property.CanWrite)
            .OrderBy(property => property.Name);

        public Dictionary<string, Table> MapTablesByName<TModel>()
            where TModel : IDataModel =>
                typeof(TModel)
                .GetProperties()
                .Select(property => ProcessModelPropertyByName(property))
                .ToDictionary(key => key.name, value => value.table);
        private (string name, Table table) ProcessModelPropertyByName(PropertyInfo propertyInfo)
        {
            Type tableType = ParseModelPropertyToTableType(propertyInfo);
            Type entityType = ExtractEntityType(tableType);

            string tableName = ExtractTableName(propertyInfo);
            return (tableName, BuildTable(entityType, tableName));
        }

        public Dictionary<Type, Table> MapTablesByType<TModel>()
            where TModel : IDataModel =>
                typeof(TModel)
                .GetProperties()
                .Select(property => ProcessModelPropertyByType(property))
                .ToDictionary(key => key.type, value => value.table);
        private (Type type, Table table) ProcessModelPropertyByType(PropertyInfo propertyInfo)
        {
            Type tableType = ParseModelPropertyToTableType(propertyInfo);
            Type entityType = ExtractEntityType(tableType);

            return (entityType, BuildTable(entityType, ExtractTableName(propertyInfo)));
        }

        public Dictionary<Type, ValueEntityDelegate> MapValueEntityDelegates<TModel>()
            where TModel : IDataModel =>
                typeof(TModel)
                .GetProperties()
                .Select(property => ProcessValueEntityDelegateByType(property))
                .ToDictionary(key => key.type, value => value.valueEntityDelegate);
        private (Type type, ValueEntityDelegate valueEntityDelegate) ProcessValueEntityDelegateByType(PropertyInfo propertyInfo)
        {
            Type tableType = ParseModelPropertyToTableType(propertyInfo);
            Type entityType = ExtractEntityType(tableType);
            return (entityType, BuildValueEntityDelegate(entityType));
        }
        private ValueEntityDelegate BuildValueEntityDelegate(Type entityType)
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

            foreach (PropertyInfo propertyInfo in ValuePropertiesForEntityType(entityType))
                builderExpressions.Add(AddValueExpression(propertyInfo, entityParameter, valueEntityParameter));

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
