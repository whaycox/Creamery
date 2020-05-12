using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Curds.Persistence.Query.Implementation
{
    using Abstraction;
    using Domain;
    using Persistence.Abstraction;

    internal class InsertQuery<TEntity> : ISqlQuery
        where TEntity : IEntity
    {
        public ISqlTable Table { get; set; }
        public List<TEntity> Entities { get; set; } = new List<TEntity>();
        private List<ValueEntity> ValueEntities => Entities
            .Select(entity => Table.BuildValueEntity(entity))
            .ToList();

        public void Write(ISqlQueryWriter queryWriter)
        {
            queryWriter.CreateTemporaryIdentityTable(Table);
            queryWriter.Insert(Table);
            queryWriter.OutputIdentitiesToTemporaryTable(Table);
            queryWriter.ValueEntities(ValueEntities);
            queryWriter.SelectTemporaryIdentityTable(Table);
            queryWriter.DropTemporaryIdentityTable(Table);
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
            Table.AssignIdentities(queryReader, entity);
        }
    }
}
