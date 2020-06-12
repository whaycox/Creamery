using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Curds.Persistence.Query.Queries.Implementation
{
    using Persistence.Abstraction;
    using Abstraction;
    using Query.Domain;

    internal class EntityUpdateQuery<TModel, TEntity> : BaseSqlQuery<TModel>, IEntityUpdate<TEntity>
        where TModel : IDataModel
        where TEntity : IEntity
    {
        private ISqlQueryContext<TModel> QueryContext { get; }

        public ISqlTable UpdatedTable { get; set; }
        public ISqlUniverse<TEntity> Source { get; set; }

        private List<ISqlQueryToken> SetTokens { get; } = new List<ISqlQueryToken>();

        public EntityUpdateQuery(ISqlQueryContext<TModel> queryContext)
            : base(queryContext)
        {
            QueryContext = queryContext;
        }

        protected override IEnumerable<ISqlQueryToken> GenerateTokens()
        {
            yield return UpdateTableToken(UpdatedTable);
            yield return QueryContext.TokenFactory.SetValues(SetTokens);
            foreach (ISqlQueryToken token in FromUniverseTokens(Source))
                yield return token;
        }

        public IEntityUpdate<TEntity> Set<TValue>(Expression<Func<TEntity, TValue>> propertyExpression, TValue newValue)
        {
            ISqlQueryToken identifierToken = QueryContext.ParseQueryExpression(propertyExpression);
            ISqlQueryToken newValueToken = QueryContext.TokenFactory.Parameter(QueryContext.ParameterBuilder, nameof(newValue), newValue);
            SetTokens.Add(SetValueToken(identifierToken, newValueToken));
            return this;
        }
        private ISqlQueryToken SetValueToken(ISqlQueryToken identifierToken, ISqlQueryToken newValueToken) =>
            QueryContext.TokenFactory.ArithmeticOperation(ArithmeticOperation.Equals, identifierToken, newValueToken);
    }
}
