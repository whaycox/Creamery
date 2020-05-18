using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace Curds.Persistence.Query.Abstraction
{
    using Persistence.Abstraction;

    internal interface ISqlQueryContext<TModel>
        where TModel : IDataModel
    {
        ISqlQueryTokenFactory TokenFactory { get; }
        ISqlQueryFormatter Formatter { get; }
        ISqlQueryParameterBuilder ParameterBuilder { get; }

        IList<ISqlTable> Tables { get; }

        ISqlTable AddTable<TEntity>() where TEntity : IEntity;

        ISqlQueryToken ParseQueryExpression(Expression queryExpression);
        ISqlTable ParseTableExpression(Expression tableExpression);
    }
}
