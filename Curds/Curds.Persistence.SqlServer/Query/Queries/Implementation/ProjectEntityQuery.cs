using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Curds.Persistence.Query.Queries.Implementation
{
    using Abstraction;
    using Persistence.Domain;
    using Model.Domain;
    using Persistence.Abstraction;
    using Model.Abstraction;

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
