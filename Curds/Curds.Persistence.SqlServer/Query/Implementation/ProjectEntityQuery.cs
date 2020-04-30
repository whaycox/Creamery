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
        public IEntityModel Model { get; set; }

        public List<TEntity> Results { get; private set; }

        public void Write(ISqlQueryWriter queryWriter)
        {
            queryWriter.Select(Model.Values.ToList());
            queryWriter.From(Model);
        }

        public async Task ProcessResult(ISqlQueryReader queryReader)
        {
            List<TEntity> results = new List<TEntity>();
            while (await queryReader.Advance())
                results.Add((TEntity)Model.ProjectEntity(queryReader));
            Results = results;
        }
    }
}
