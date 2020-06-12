using System.Collections.Generic;
using System.Threading.Tasks;

namespace Curds.Persistence.Query.Queries.Implementation
{
    using Abstraction;
    using Persistence.Abstraction;

    internal class ProjectEntityQuery<TModel, TEntity> : BaseSqlQuery<TModel, TEntity>
        where TModel : IDataModel
        where TEntity : IEntity
    {
        public ISqlTable ProjectedTable { get; set; }
        public ISqlUniverse<TEntity> Source { get; set; }

        public ProjectEntityQuery(ISqlQueryContext<TModel> queryContext)
            : base(queryContext)
        { }

        protected override IEnumerable<ISqlQueryToken> GenerateTokens()
        {
            yield return SelectColumnsToken(ProjectedTable.Columns);
            foreach (ISqlQueryToken token in FromUniverseTokens(Source))
                yield return token;
        }

        public override async Task ProcessResult(ISqlQueryReader queryReader)
        {
            while (await queryReader.Advance())
                Results.Add((TEntity)ProjectedTable.ProjectEntity(queryReader));
        }
    }
}
