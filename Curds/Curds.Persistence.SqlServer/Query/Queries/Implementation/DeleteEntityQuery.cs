using System.Collections.Generic;

namespace Curds.Persistence.Query.Queries.Implementation
{
    using Abstraction;
    using Persistence.Abstraction;
    using Query.Abstraction;

    internal class DeleteEntityQuery<TModel, TEntity> : BaseSqlQuery<TModel>
        where TModel : IDataModel
        where TEntity : IEntity
    {
        private ISqlQueryPhraseBuilder PhraseBuilder { get; }

        public ISqlTable DeletedTable { get; }
        public ISqlUniverse Source { get; }

        public DeleteEntityQuery(
            ISqlQueryContext<TModel> queryContext,
            ISqlQueryPhraseBuilder phraseBuilder,
            ISqlTable deletedTable,
            ISqlUniverse source)
            : base(queryContext)
        {
            PhraseBuilder = phraseBuilder;

            DeletedTable = deletedTable;
            Source = source;
        }

        protected override IEnumerable<ISqlQueryToken> GenerateTokens()
        {
            yield return PhraseBuilder.DeleteTableToken(DeletedTable);
            foreach (ISqlQueryToken token in PhraseBuilder.FromUniverseTokens(Source))
                yield return token;
        }
    }
}
