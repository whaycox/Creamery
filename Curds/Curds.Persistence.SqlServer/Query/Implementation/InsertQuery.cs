using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace Curds.Persistence.Query.Implementation
{
    using Abstraction;
    using Domain;
    using Persistence.Domain;
    using Model.Domain;
    using Model.Abstraction;
    using Persistence.Abstraction;

    internal class InsertQuery<TEntity> : ISqlQuery
        where TEntity : IEntity
    {
        public IEntityModel<TEntity> Model { get; set; }
        public List<TEntity> Entities { get; set; } = new List<TEntity>();
        private List<ValueEntity<TEntity>> ValueEntities => Entities
            .Select(entity => Model.ValueEntity(entity))
            .ToList();

        public void Write(ISqlQueryWriter queryWriter)
        {
            Table table = Model.Table();

            queryWriter.CreateTemporaryIdentityTable(table);
            queryWriter.Insert(table);
            queryWriter.OutputIdentitiesToTemporaryTable(table);
            queryWriter.ValueEntities(ValueEntities);
            queryWriter.SelectTemporaryIdentityTable(table);
            queryWriter.DropTemporaryIdentityTable(table);
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
            Model.AssignIdentityDelegate(queryReader, entity);
        }
    }
}
