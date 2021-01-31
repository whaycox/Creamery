using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Curds.Persistence.Query.Abstraction
{
    using Persistence.Abstraction;

    public interface ISqlUniverse<TDataModel>
        where TDataModel : IDataModel
    {
        ISqlQueryContext<TDataModel> QueryContext { get; }
        IEnumerable<ISqlQueryToken> Tokens { get; }
    }

    public interface ISqlUniverse<TDataModel, TEntity> : ISqlUniverse<TDataModel>
        where TDataModel : IDataModel
        where TEntity : class, IEntity
    {
        ISqlQuery<TEntity> Project();
        ISqlQuery Delete();

        ISqlJoinClause<TDataModel, TEntity, ISqlUniverse<TDataModel, TEntity>, TJoinedEntity> Join<TJoinedEntity>(Expression<Func<TDataModel, TJoinedEntity>> entitySelectionExpression)
            where TJoinedEntity : class, IEntity;
        ISqlUniverse<TDataModel, TEntity, TJoinedEntity> AddJoin<TUniverse, TJoinedEntity>(ISqlJoinClause<TDataModel, TEntity, TUniverse, TJoinedEntity> joinClause)
            where TUniverse : ISqlUniverse<TDataModel, TEntity>
            where TJoinedEntity : class, IEntity;
        ISqlUniverse<TDataModel, TEntity> Where(Expression<Func<TEntity, bool>> filterExpression);

        IEntityUpdate<TEntity> Update();
    }

    public interface ISqlUniverse<TDataModel, TEntityOne, TEntityTwo> : ISqlUniverse<TDataModel>
        where TDataModel : IDataModel
        where TEntityOne : class, IEntity
        where TEntityTwo : class, IEntity
    {
        ISqlQuery<TEntity> Project<TEntity>(Expression<Func<TEntityOne, TEntityTwo, TEntity>> entityProjectionExpression)
            where TEntity : class, IEntity;

        ISqlUniverse<TDataModel, TEntityOne, TEntityTwo> Where(Expression<Func<TEntityOne, TEntityTwo, bool>> filterExpression);
    }
}
