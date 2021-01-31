using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Curds.Persistence.Query.Queries.Implementation
{
    using Abstraction;
    using Persistence.Abstraction;
    using Query.Domain;

    internal class EntityUpdateQuery<TDataModel, TEntity> : BaseSqlQuery<TDataModel>, IEntityUpdate<TEntity>
        where TDataModel : IDataModel
        where TEntity : class, IEntity
    {
        private ISqlQueryContext<TDataModel> QueryContext { get; }

        public ISqlTable UpdatedTable { get; }
        public ISqlUniverse<TDataModel> Source { get; }

        public List<ISqlQueryToken> SetTokens { get; } = new List<ISqlQueryToken>();

        public EntityUpdateQuery(
            ISqlQueryContext<TDataModel> queryContext,
            ISqlTable updatedTable,
            ISqlUniverse<TDataModel> source)
            : base(queryContext)
        {
            QueryContext = queryContext;
            UpdatedTable = updatedTable;
            Source = source;
        }

        protected override IEnumerable<ISqlQueryToken> GenerateTokens()
        {
            yield return PhraseBuilder.UpdateTableToken(UpdatedTable, SetTokens);
            foreach (ISqlQueryToken token in Source.Tokens)
                yield return token;
        }

        public IEntityUpdate<TEntity> Set<TValue>(Expression<Func<TEntity, TValue>> propertyExpression, TValue newValue)
        {
            ISqlQueryToken identifierToken = QueryContext.ParseQueryExpression(propertyExpression);
            ISqlQueryToken newValueToken = TokenFactory.Parameter(QueryContext.ParameterBuilder, nameof(newValue), newValue);
            SetTokens.Add(SetValueToken(identifierToken, newValueToken));
            return this;
        }
        private ISqlQueryToken SetValueToken(ISqlQueryToken identifierToken, ISqlQueryToken newValueToken) =>
            TokenFactory.ArithmeticOperation(ArithmeticOperation.Equals, identifierToken, newValueToken);
    }
}
