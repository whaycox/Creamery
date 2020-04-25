using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Curds.Persistence.Query.Implementation
{
    using Abstraction;
    using Persistence.Domain;
    using Model.Domain;

    internal class ProjectEntityQuery<TEntity> : ISqlQuery<TEntity>
        where TEntity : BaseEntity
    {
        public Table ProjectedTable { get; set; }

        public List<TEntity> Results => throw new NotImplementedException();

        public void Write(ISqlQueryWriter queryWriter)
        {
            queryWriter.Select(ProjectedTable.Columns);
            queryWriter.From(ProjectedTable);
        }

        public Task ProcessResult(ISqlQueryReader queryReader)
        {
            throw new NotImplementedException();
        }
    }
}
