using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace Curds.Persistence.Query.Template
{
    using Implementation;
    using Persistence.Domain;

    public abstract class BaseQueryExpressionVisitorTemplate
    {
        protected Expression TestExpressionOne = Expression.Constant(1);
        protected ParameterExpression TestParameterExpression = Expression.Parameter(typeof(TestEntity), nameof(TestParameterExpression));
    }
}
