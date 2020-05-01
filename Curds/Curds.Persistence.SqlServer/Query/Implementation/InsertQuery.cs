using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Curds.Persistence.Query.Implementation
{
    using Abstraction;
    using Domain;
    using Model.Abstraction;
    using Persistence.Abstraction;

    internal class InsertQuery<TEntity> : ISqlQuery
        where TEntity : IEntity
    {
        public IEntityModel Model { get; set; }
        public List<TEntity> Entities { get; set; } = new List<TEntity>();
        private List<ValueEntity> ValueEntities => Entities
            .Select(entity => Model.ValueEntity(entity))
            .ToList();

        public void Write(ISqlQueryWriter queryWriter)
        {
            queryWriter.CreateTemporaryIdentityTable(Model);
            queryWriter.Insert(Model);
            queryWriter.OutputIdentitiesToTemporaryTable(Model);
            queryWriter.ValueEntities(ValueEntities);
            queryWriter.SelectTemporaryIdentityTable(Model);
            queryWriter.DropTemporaryIdentityTable(Model);
        }

        public async Task ProcessResult(ISqlQueryReader queryReader)
        {
            for (int i = 0; i < Entities.Count; i++)
                await AssignIdentity(queryReader, Entities[i]);
            if (await queryReader.Advance())
                throw new InvalidOperationException("There were more new identities than inserted entities");
        }
        private async Task AssignIdentity(ISqlQueryReader queryReader, TEntity entity)
        {
            if (!await queryReader.Advance())
                throw new InvalidOperationException("There were less new identities than inserted entities");
            Model.AssignIdentity(queryReader, entity);
        }
    }
}
