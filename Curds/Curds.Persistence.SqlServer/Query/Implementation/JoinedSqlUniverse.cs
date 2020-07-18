using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace Curds.Persistence.Query.Implementation
{
    using Persistence.Abstraction;
    using Abstraction;
    using Queries.Implementation;

    internal class JoinedSqlUniverse<TDataModel, TEntityOne, TEntityTwo> : ISqlUniverse<TDataModel, TEntityOne, TEntityTwo>
        where TDataModel : IDataModel
        where TEntityOne : IEntity
        where TEntityTwo : IEntity
    {
        private ISqlUniverse<TDataModel> SourceUniverse { get; }

        public ISqlQueryContext<TDataModel> QueryContext => SourceUniverse.QueryContext;
        public IEnumerable<ISqlQueryToken> Tokens => SourceUniverse.Tokens;

        public JoinedSqlUniverse(ISqlUniverse<TDataModel, TEntityOne> sourceUniverse)
        {
            SourceUniverse = sourceUniverse;
        }

        public ISqlQuery<TEntity> Project<TEntity>(Expression<Func<TEntityOne, TEntityTwo, TEntity>> entityProjectionExpression)
            where TEntity : IEntity => new ProjectEntityQuery<TDataModel, TEntity>(
                QueryContext,
                QueryContext.ParseTableExpression(entityProjectionExpression),
                this);
    }
}
