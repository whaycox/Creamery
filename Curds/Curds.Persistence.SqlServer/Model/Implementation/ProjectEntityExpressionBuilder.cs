using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;

namespace Curds.Persistence.Model.Implementation
{
    using Abstraction;
    using Persistence.Abstraction;
    using Query.Abstraction;

    internal delegate Expression PopulateValueDelegate(ParameterExpression entityParameter, PropertyInfo valueProperty, ParameterExpression queryReaderParameter);

    internal class ProjectEntityExpressionBuilder : BaseExpressionBuilder, IProjectEntityExpressionBuilder
    {
        private Dictionary<Type, PopulateValueDelegate> PopulateTypeMap { get; }

        public ProjectEntityExpressionBuilder()
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

        private Expression PopulateBoolValue(ParameterExpression entityParameter, PropertyInfo valueProperty, ParameterExpression queryReaderParameter) => 
            Expression.Throw(Expression.Constant(new NotImplementedException()));
        private Expression PopulateNullableBoolValue(ParameterExpression entityParameter, PropertyInfo valueProperty, ParameterExpression queryReaderParameter) =>
            Expression.Throw(Expression.Constant(new NotImplementedException()));
        private Expression PopulateByteValue(ParameterExpression entityParameter, PropertyInfo valueProperty, ParameterExpression queryReaderParameter) =>
            Expression.Throw(Expression.Constant(new NotImplementedException()));
        private Expression PopulateNullableByteValue(ParameterExpression entityParameter, PropertyInfo valueProperty, ParameterExpression queryReaderParameter) =>
            Expression.Throw(Expression.Constant(new NotImplementedException()));
        private Expression PopulateShortValue(ParameterExpression entityParameter, PropertyInfo valueProperty, ParameterExpression queryReaderParameter) =>
            Expression.Throw(Expression.Constant(new NotImplementedException()));
        private Expression PopulateNullableShortValue(ParameterExpression entityParameter, PropertyInfo valueProperty, ParameterExpression queryReaderParameter) =>
            Expression.Throw(Expression.Constant(new NotImplementedException()));
        private Expression PopulateNullableIntValue(ParameterExpression entityParameter, PropertyInfo valueProperty, ParameterExpression queryReaderParameter) =>
            Expression.Throw(Expression.Constant(new NotImplementedException()));
        private Expression PopulateLongValue(ParameterExpression entityParameter, PropertyInfo valueProperty, ParameterExpression queryReaderParameter) =>
            Expression.Throw(Expression.Constant(new NotImplementedException()));
        private Expression PopulateNullableLongValue(ParameterExpression entityParameter, PropertyInfo valueProperty, ParameterExpression queryReaderParameter) =>
            Expression.Throw(Expression.Constant(new NotImplementedException()));
        private Expression PopulateDateTimeValue(ParameterExpression entityParameter, PropertyInfo valueProperty, ParameterExpression queryReaderParameter) =>
            Expression.Throw(Expression.Constant(new NotImplementedException()));
        private Expression PopulateNullableDateTimeValue(ParameterExpression entityParameter, PropertyInfo valueProperty, ParameterExpression queryReaderParameter) =>
            Expression.Throw(Expression.Constant(new NotImplementedException()));
        private Expression PopulateDateTimeOffsetValue(ParameterExpression entityParameter, PropertyInfo valueProperty, ParameterExpression queryReaderParameter) =>
            Expression.Throw(Expression.Constant(new NotImplementedException()));
        private Expression PopulateNullableDateTimeOffsetValue(ParameterExpression entityParameter, PropertyInfo valueProperty, ParameterExpression queryReaderParameter) =>
            Expression.Throw(Expression.Constant(new NotImplementedException()));
        private Expression PopulateDecimalValue(ParameterExpression entityParameter, PropertyInfo valueProperty, ParameterExpression queryReaderParameter) =>
            Expression.Throw(Expression.Constant(new NotImplementedException()));
        private Expression PopulateNullableDecimalValue(ParameterExpression entityParameter, PropertyInfo valueProperty, ParameterExpression queryReaderParameter) =>
            Expression.Throw(Expression.Constant(new NotImplementedException()));
        private Expression PopulateDoubleValue(ParameterExpression entityParameter, PropertyInfo valueProperty, ParameterExpression queryReaderParameter) =>
            Expression.Throw(Expression.Constant(new NotImplementedException()));
        private Expression PopulateNullableDoubleValue(ParameterExpression entityParameter, PropertyInfo valueProperty, ParameterExpression queryReaderParameter) =>
            Expression.Throw(Expression.Constant(new NotImplementedException()));

        private Expression PopulateIntValue(ParameterExpression entityParameter, PropertyInfo valueProperty, ParameterExpression queryReaderParameter)
        {
            MethodInfo readIntMethod = typeof(ISqlQueryReader).GetMethod(nameof(ISqlQueryReader.ReadInt));
            Expression readIntExpression = Expression.Call(queryReaderParameter, readIntMethod, Expression.Constant(0, typeof(int)));
            MethodInfo nullableIntValueMethod = typeof(int?).GetProperty(nameof(Nullable<int>.Value)).GetMethod;
            Expression nullableIntValueExpression = Expression.Call(readIntExpression, nullableIntValueMethod);
            return Expression.Call(entityParameter, valueProperty.SetMethod, nullableIntValueExpression);
        }

        private Expression PopulateStringValue(ParameterExpression entityParameter, PropertyInfo valueProperty, ParameterExpression queryReaderParameter)
        {
            MethodInfo readStringMethod = typeof(ISqlQueryReader).GetMethod(nameof(ISqlQueryReader.ReadString));
            Expression readStringExpression = Expression.Call(queryReaderParameter, readStringMethod, Expression.Constant(1, typeof(int)));
            return Expression.Call(entityParameter, valueProperty.SetMethod, readStringExpression);
        }

        public ProjectEntityDelegate<IEntity> BuildProjectEntityDelegate(Type entityType, IEnumerable<PropertyInfo> valueProperties)
        {
            ParameterExpression queryReaderParameter = Expression.Parameter(typeof(ISqlQueryReader), nameof(queryReaderParameter));
            ParameterExpression entityParameter = Expression.Parameter(entityType, nameof(entityParameter));
            List<ParameterExpression> projectionExpressionParameters = new List<ParameterExpression>
            {
                entityParameter,
            };

            ConstructorInfo entityConstructor = entityType.GetConstructor(new Type[0]);
            if (entityConstructor == null)
                throw new Exception();

            List<Expression> projectionExpressions = new List<Expression>
            {
                Expression.Assign(entityParameter, Expression.New(entityConstructor)),
            };
            foreach (PropertyInfo valueProperty in valueProperties)
                projectionExpressions.Add(PopulateValueFromReader(entityParameter, valueProperty, queryReaderParameter));

            LabelTarget returnLabel = Expression.Label(entityType);
            projectionExpressions.Add(Expression.Return(returnLabel, entityParameter));
            projectionExpressions.Add(Expression.Label(returnLabel, entityParameter));

            BlockExpression projectionBlock = Expression.Block(projectionExpressionParameters, projectionExpressions);

            Type delegateType = typeof(ProjectEntityDelegate<>);
            delegateType = delegateType.MakeGenericType(entityType);
            Delegate projection = Expression
                .Lambda(delegateType, projectionBlock, queryReaderParameter)
                .Compile();
            return projection as ProjectEntityDelegate<IEntity>;
        }
        private Expression PopulateValueFromReader(ParameterExpression entityParameter, PropertyInfo valueProperty, ParameterExpression queryReaderParameter)
        {
            if (!PopulateTypeMap.TryGetValue(valueProperty.PropertyType, out PopulateValueDelegate populateDelegate))
                throw new ArgumentException($"Unsupported type for {nameof(valueProperty)}, {valueProperty.PropertyType}");
            return populateDelegate(entityParameter, valueProperty, queryReaderParameter);
        }
    }
}
