using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Curds.Persistence.Query.Implementation
{
    using Abstraction;
    using Curds.Persistence.Abstraction;
    using Domain;
    using Queries.Implementation;

    internal class SqlUniverse<TDataModel, TEntity> : ISqlUniverse<TDataModel, TEntity>
        where TDataModel : IDataModel
        where TEntity : IEntity
    {
        private ISqlTable Table { get; }

        private List<ISqlQueryToken> FilterCollection { get; } = new List<ISqlQueryToken>();
        private List<ISqlJoinClause> JoinCollection { get; } = new List<ISqlJoinClause>();

        private ISqlQueryTokenFactory TokenFactory => QueryContext.TokenFactory;
        private List<ISqlTable> Tables => QueryContext.Tables;

        public ISqlQueryContext<TDataModel> QueryContext { get; }
        public IEnumerable<ISqlQueryToken> Tokens
        {
            get
            {
                if (Tables.Count != JoinCollection.Count + 1)
                    throw new InvalidOperationException("Not the right number of joins for tables");

                for (int i = 0; i < Tables.Count; i++)
                {
                    if (i == 0)
                    {
                        yield return TokenFactory.Phrase(
                            TokenFactory.Keyword(SqlQueryKeyword.FROM),
                            TokenFactory.QualifiedObjectName(Tables[i]));
                    }
                    else
                    {
                        yield return TokenFactory.Phrase(
                            TokenFactory.Keyword(SqlQueryKeyword.JOIN),
                            TokenFactory.QualifiedObjectName(Tables[i]));
                        yield return TokenFactory.JoinClause(JoinCollection[i - 1]);
                    }
                }

                for (int i = 0; i < FilterCollection.Count; i++)
                    yield return TokenFactory.Phrase(
                        TokenFactory.Keyword(i > 0 ? SqlQueryKeyword.AND : SqlQueryKeyword.WHERE),
                        FilterCollection[i]);
            }
        }

        public SqlUniverse(ISqlQueryContext<TDataModel> queryContext)
        {
            QueryContext = queryContext;
            Table = queryContext.AddTable<TEntity>();
        }

        public ISqlQuery<TEntity> Project() => new ProjectEntityQuery<TDataModel, TEntity>(
            QueryContext,
            Table,
            this);

        public ISqlQuery Delete() => new DeleteEntityQuery<TDataModel, TEntity>(
            QueryContext,
            Table,
            this);

        public ISqlUniverse<TDataModel, TEntity> Where(Expression<Func<TEntity, bool>> filterExpression)
        {
            FilterCollection.Add(QueryContext.ParseQueryExpression(filterExpression));
            return this;
        }

        public IEntityUpdate<TEntity> Update() => new EntityUpdateQuery<TDataModel, TEntity>(
            QueryContext,
            Table,
            this);

        public ISqlJoinClause<TDataModel, TEntity, ISqlUniverse<TDataModel, TEntity>, TJoinedEntity> Join<TJoinedEntity>(Expression<Func<TDataModel, TJoinedEntity>> entitySelectionExpression)
            where TJoinedEntity : IEntity => new SqlJoinClause<TDataModel, TEntity, ISqlUniverse<TDataModel, TEntity>, TJoinedEntity>(
                QueryContext,
                this);

        public ISqlUniverse<TDataModel, TEntity, TJoinedEntity> AddJoin<TUniverse, TJoinedEntity>(ISqlJoinClause<TDataModel, TEntity, TUniverse, TJoinedEntity> joinClause)
            where TUniverse : ISqlUniverse<TDataModel, TEntity>
            where TJoinedEntity : IEntity
        {
            JoinCollection.Add(joinClause);
            return new JoinedSqlUniverse<TDataModel, TEntity, TJoinedEntity>(this);
        }
    }
}
