using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Curds.Persistence.Query.Implementation
{
    using Abstraction;
    using Persistence.Domain;
    using Model.Domain;
    using Persistence.Abstraction;
    using Model.Abstraction;

    internal class ProjectEntityQuery<TEntity> : ISqlQuery<TEntity>
        where TEntity : IEntity
    {
        public ISqlTable ProjectedTable { get; set; }
        public ISqlUniverse<TEntity> Source { get; set; }

        public IList<TEntity> Results { get; private set; }

        public void Write(ISqlQueryWriter queryWriter)
        {
            queryWriter.Select(ProjectedTable.Values);
            queryWriter.From(Source);
        }

        public async Task ProcessResult(ISqlQueryReader queryReader)
        {
            List<TEntity> results = new List<TEntity>();
            while (await queryReader.Advance())
                results.Add((TEntity)ProjectedTable.ProjectEntity(queryReader));
            Results = results;
        }
    }
}
