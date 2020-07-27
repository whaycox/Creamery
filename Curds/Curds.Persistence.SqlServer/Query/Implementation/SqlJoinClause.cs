﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Linq;

namespace Curds.Persistence.Query.Implementation
{
    using Abstraction;
    using Persistence.Abstraction;
    using Domain;

    internal class SqlJoinClause<TDataModel, TEntity, TUniverse, TJoinedEntity> : ISqlJoinClause<TDataModel, TEntity, TUniverse, TJoinedEntity>
        where TDataModel : IDataModel
        where TEntity : IEntity
        where TUniverse : ISqlUniverse<TDataModel, TEntity>
        where TJoinedEntity : IEntity
    {
        private ISqlQueryContext<TDataModel> QueryContext { get; }
        private TUniverse Universe { get; }
        private JoinType JoinType { get; set; }

        private List<ISqlQueryToken> ClauseCollection { get; } = new List<ISqlQueryToken>();

        private ISqlQueryTokenFactory TokenFactory => QueryContext.TokenFactory;

        public ISqlTable JoinedTable { get; }

        public IEnumerable<ISqlQueryToken> Tokens
        {
            get
            {
                for (int i = 0; i < ClauseCollection.Count; i++)
                    yield return TokenFactory.Phrase(
                        TokenFactory.Keyword(i == 0 ? SqlQueryKeyword.ON : SqlQueryKeyword.AND),
                        ClauseCollection[i]);
            }
        }

        public SqlJoinClause(
            ISqlQueryContext<TDataModel> queryContext,
            TUniverse universe)
        {
            QueryContext = queryContext;
            Universe = universe;

            JoinedTable = QueryContext.AddTable<TJoinedEntity>();
        }

        public ISqlUniverse<TDataModel, TEntity, TJoinedEntity> Inner()
        {
            JoinType = JoinType.Inner;
            return Universe.AddJoin(this);
        }

        public ISqlJoinClause<TDataModel, TEntity, TUniverse, TJoinedEntity> On(Expression<Func<TEntity, TJoinedEntity, bool>> clauseExpression)
        {
            ClauseCollection.Add(QueryContext.ParseQueryExpression(clauseExpression));
            return this;
        }
    }
}