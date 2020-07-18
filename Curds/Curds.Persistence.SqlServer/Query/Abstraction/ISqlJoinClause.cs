using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace Curds.Persistence.Query.Abstraction
{
    using Persistence.Abstraction;

    public interface ISqlJoinClause
    { }

    public interface ISqlJoinClause<TDataModel, TEntity, TUniverse, TJoinedEntity> : ISqlJoinClause
        where TDataModel : IDataModel
        where TEntity : IEntity
        where TUniverse : ISqlUniverse<TDataModel, TEntity>
        where TJoinedEntity : IEntity
    {
        ISqlTable JoinedTable { get; }

        ISqlJoinClause<TDataModel, TEntity, TUniverse, TJoinedEntity> On(Expression<Func<TEntity, TJoinedEntity, bool>> clauseExpression);

        ISqlUniverse<TDataModel, TEntity, TJoinedEntity> Inner();
    }
}
