using System.Collections.Generic;

namespace Curds.Persistence.Query.Queries.Implementation
{
    using Abstraction;
    using Persistence.Abstraction;

    internal class DeleteEntityQuery<TModel, TEntity> : BaseSqlQuery<TModel>
        where TModel : IDataModel
        where TEntity : IEntity
    {
        public ISqlTable DeletedTable { get; set; }
        public ISqlUniverse<TEntity> Source { get; set; }

        public DeleteEntityQuery(ISqlQueryContext<TModel> queryContext)
            : base(queryContext)
        { }

        protected override IEnumerable<ISqlQueryToken> GenerateTokens()
        {
            yield return DeleteTableToken(DeletedTable);
            foreach (ISqlQueryToken token in FromUniverseTokens(Source))
                yield return token;
        }
    }
}
