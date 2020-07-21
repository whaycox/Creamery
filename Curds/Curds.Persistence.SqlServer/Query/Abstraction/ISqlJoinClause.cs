using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace Curds.Persistence.Query.Abstraction
{
    using Persistence.Abstraction;
    using Domain;

    public interface ISqlJoinClause
    {
        ISqlTable JoinedTable { get; }
        IEnumerable<ISqlQueryToken> Tokens { get; }
    }

    public interface ISqlJoinClause<TDataModel, TEntity, TUniverse, TJoinedEntity> : ISqlJoinClause
        where TDataModel : IDataModel
        where TEntity : IEntity
        where TUniverse : ISqlUniverse<TDataModel, TEntity>
        where TJoinedEntity : IEntity
    {
        ISqlJoinClause<TDataModel, TEntity, TUniverse, TJoinedEntity> On(Expression<Func<TEntity, TJoinedEntity, bool>> clauseExpression);

        ISqlUniverse<TDataModel, TEntity, TJoinedEntity> Inner();
    }
}
