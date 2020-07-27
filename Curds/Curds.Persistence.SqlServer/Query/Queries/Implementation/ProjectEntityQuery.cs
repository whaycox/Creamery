using System.Collections.Generic;
using System.Threading.Tasks;

namespace Curds.Persistence.Query.Queries.Implementation
{
    using Abstraction;
    using Persistence.Abstraction;

    internal class ProjectEntityQuery<TDataModel, TEntity> : BaseSqlQuery<TDataModel, TEntity>
        where TDataModel : IDataModel
        where TEntity : IEntity
    {
        public ISqlTable ProjectedTable { get; }
        public ISqlUniverse<TDataModel> Source { get; }

        public ProjectEntityQuery(
            ISqlQueryContext<TDataModel> queryContext,
            ISqlTable projectedTable,
            ISqlUniverse<TDataModel> source)
            : base(queryContext)
        {
            ProjectedTable = projectedTable;
            Source = source;
        }

        protected override IEnumerable<ISqlQueryToken> GenerateTokens()
        {
            yield return PhraseBuilder.SelectColumnsToken(ProjectedTable.Columns);
            foreach (ISqlQueryToken token in Source.Tokens)
                yield return token;
        }

        public override async Task ProcessResult(ISqlQueryReader queryReader)
        {
            while (await queryReader.Advance())
                Results.Add((TEntity)ProjectedTable.ProjectEntity(queryReader));
        }
    }
}
