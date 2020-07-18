using System.Collections.Generic;
using System.Linq.Expressions;

namespace Curds.Persistence.Query.Abstraction
{
    using Persistence.Abstraction;

    public interface ISqlQueryContext<TModel>
        where TModel : IDataModel
    {
        ISqlQueryFormatter Formatter { get; }
        ISqlConnectionContext ConnectionContext { get; }
        ISqlQueryParameterBuilder ParameterBuilder { get; }
        ISqlQueryTokenFactory TokenFactory { get; }
        ISqlQueryPhraseBuilder PhraseBuilder { get; }

        List<ISqlTable> Tables { get; }

        ISqlTable AddTable<TEntity>() where TEntity : IEntity;

        ISqlQueryToken ParseQueryExpression(Expression queryExpression);
        ISqlTable ParseTableExpression(Expression tableExpression);
    }
}
