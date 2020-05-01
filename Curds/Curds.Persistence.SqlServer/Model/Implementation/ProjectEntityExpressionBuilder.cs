using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Curds.Persistence.Model.Implementation
{
    using Abstraction;
    using Query.Abstraction;

    internal class ProjectEntityExpressionBuilder : BaseQueryReaderExpressionBuilder, IProjectEntityExpressionBuilder
    {
        public ProjectEntityDelegate BuildProjectEntityDelegate(IEntityModel entityModel)
        {
            ParameterExpression queryReaderParameter = Expression.Parameter(typeof(ISqlQueryReader), nameof(queryReaderParameter));
            ParameterExpression entityParameter = Expression.Parameter(entityModel.EntityType, nameof(entityParameter));
            List<ParameterExpression> projectionExpressionParameters = new List<ParameterExpression>
            {
                entityParameter,
            };

            ConstructorInfo entityConstructor = entityModel.EntityType.GetConstructor(new Type[0]);
            if (entityConstructor == null)
                throw new Exception();

            List<Expression> projectionExpressions = new List<Expression>
            {
                Expression.Assign(entityParameter, Expression.New(entityConstructor)),
            };
            foreach (IValueModel value in entityModel.Values)
                projectionExpressions.Add(PopulateValueFromReader(entityParameter, value, queryReaderParameter));

            LabelTarget returnLabel = Expression.Label(entityModel.EntityType);
            projectionExpressions.Add(Expression.Return(returnLabel, entityParameter));
            projectionExpressions.Add(Expression.Label(returnLabel, entityParameter));

            BlockExpression projectionBlock = Expression.Block(projectionExpressionParameters, projectionExpressions);

            return Expression
                .Lambda<ProjectEntityDelegate>(projectionBlock, queryReaderParameter)
                .Compile();
        }
    }
}
