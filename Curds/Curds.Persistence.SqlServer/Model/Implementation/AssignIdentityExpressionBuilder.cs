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

    internal class AssignIdentityExpressionBuilder : BaseQueryReaderExpressionBuilder, IAssignIdentityExpressionBuilder
    {
        public AssignIdentityDelegate BuildAssignIdentityDelegate(Type entityType, PropertyInfo identityProperty)
        {
            ParameterExpression queryReaderParameter = Expression.Parameter(typeof(ISqlQueryReader), nameof(queryReaderParameter));
            ParameterExpression iEntityParameter = Expression.Parameter(typeof(IEntity), nameof(iEntityParameter));
            ParameterExpression entityParameter = Expression.Parameter(entityType, nameof(entityParameter));
            List<ParameterExpression> builderExpressionParameters = new List<ParameterExpression>
            {
                entityParameter,
            };

            List<Expression> builderExpressions = new List<Expression>
            {
                Expression.Assign(entityParameter, Expression.Convert(iEntityParameter, entityType)),
                PopulateValueFromReader(entityParameter, identityProperty, queryReaderParameter),
            };

            BlockExpression builderBlock = Expression.Block(builderExpressionParameters, builderExpressions);

            return Expression
                .Lambda<AssignIdentityDelegate>(builderBlock, new ParameterExpression[] { queryReaderParameter, iEntityParameter })
                .Compile();
        }
    }
}
