using System.Collections.Generic;
using System.Threading.Tasks;

namespace Curds.Persistence.Query.Queries.Implementation
{
    using Abstraction;
    using Persistence.Abstraction;
    using Query.Abstraction;

    internal class ProjectEntityQuery<TModel, TEntity> : BaseSqlQuery<TModel, TEntity>
        where TModel : IDataModel
        where TEntity : IEntity
    {
        private ISqlQueryPhraseBuilder PhraseBuilder { get; }

        public ISqlTable ProjectedTable { get; }
        public ISqlUniverse Source { get; }

        public ProjectEntityQuery(
            ISqlQueryContext<TModel> queryContext,
            ISqlQueryPhraseBuilder phraseBuilder,
            ISqlTable projectedTable,
            ISqlUniverse source)
            : base(queryContext)
        {
            PhraseBuilder = phraseBuilder;

            ProjectedTable = projectedTable;
            Source = source;
        }

        protected override IEnumerable<ISqlQueryToken> GenerateTokens()
        {
            yield return PhraseBuilder.SelectColumnsToken(ProjectedTable.Columns);
            foreach (ISqlQueryToken token in PhraseBuilder.FromUniverseTokens(Source))
                yield return token;
        }

        public override async Task ProcessResult(ISqlQueryReader queryReader)
        {
            while (await queryReader.Advance())
                Results.Add((TEntity)ProjectedTable.ProjectEntity(queryReader));
        }
    }
}
