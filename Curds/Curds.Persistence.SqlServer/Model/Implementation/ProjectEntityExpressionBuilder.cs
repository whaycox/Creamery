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

    internal class ProjectEntityExpressionBuilder : BaseQueryReaderExpressionBuilder, IProjectEntityExpressionBuilder
    {
        private Expression PopulateIntValue(ParameterExpression entityParameter, PropertyInfo valueProperty, ParameterExpression queryReaderParameter)
        {
            MethodInfo readIntMethod = typeof(ISqlQueryReader).GetMethod(nameof(ISqlQueryReader.ReadInt));
            Expression readIntExpression = Expression.Call(queryReaderParameter, readIntMethod, Expression.Constant(valueProperty.Name, typeof(string)));
            MethodInfo nullableIntValueMethod = typeof(int?).GetProperty(nameof(Nullable<int>.Value)).GetMethod;
            Expression nullableIntValueExpression = Expression.Call(readIntExpression, nullableIntValueMethod);
            return Expression.Call(entityParameter, valueProperty.SetMethod, nullableIntValueExpression);
        }

        private Expression PopulateStringValue(ParameterExpression entityParameter, PropertyInfo valueProperty, ParameterExpression queryReaderParameter)
        {
            MethodInfo readStringMethod = typeof(ISqlQueryReader).GetMethod(nameof(ISqlQueryReader.ReadString));
            Expression readStringExpression = Expression.Call(queryReaderParameter, readStringMethod, Expression.Constant(valueProperty.Name, typeof(string)));
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
    }
}
