using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Curds.Persistence.Query.Queries.Implementation
{
    using Abstraction;
    using Persistence.Abstraction;
    using Query.Abstraction;
    using Query.Domain;

    internal class EntityUpdateQuery<TModel, TEntity> : BaseSqlQuery<TModel>, IEntityUpdate<TEntity>
        where TModel : IDataModel
        where TEntity : IEntity
    {
        private ISqlQueryTokenFactory TokenFactory { get; }
        private ISqlQueryPhraseBuilder PhraseBuilder { get; }
        private ISqlQueryContext<TModel> QueryContext { get; }

        public ISqlTable UpdatedTable { get; }
        public ISqlUniverse Source { get; }

        public List<ISqlQueryToken> SetTokens { get; } = new List<ISqlQueryToken>();

        public EntityUpdateQuery(
            ISqlQueryContext<TModel> queryContext,
            ISqlQueryTokenFactory tokenFactory,
            ISqlQueryPhraseBuilder phraseBuilder,
            ISqlTable updatedTable,
            ISqlUniverse source)
            : base(queryContext)
        {
            TokenFactory = tokenFactory;
            PhraseBuilder = phraseBuilder;
            QueryContext = queryContext;
            UpdatedTable = updatedTable;
            Source = source;
        }

        protected override IEnumerable<ISqlQueryToken> GenerateTokens()
        {
            yield return PhraseBuilder.UpdateTableToken(UpdatedTable);
            yield return PhraseBuilder.SetValuesToken(SetTokens);
            foreach (ISqlQueryToken token in PhraseBuilder.FromUniverseTokens(Source))
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
