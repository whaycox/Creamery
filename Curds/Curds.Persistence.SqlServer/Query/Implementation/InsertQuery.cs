using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace Curds.Persistence.Query.Implementation
{
    using Abstraction;
    using Domain;
    using Persistence.Domain;
    using Model.Domain;
    using Model.Abstraction;

    internal class InsertQuery<TEntity> : ISqlQuery
        where TEntity : BaseEntity
    {
        public Table Table { get; set; }
        public List<ValueEntity<TEntity>> Entities { get; set; } = new List<ValueEntity<TEntity>>();
        public AssignIdentityDelegate AssignIdentityDelegate { get; set; }

        public void Write(ISqlQueryWriter queryWriter)
        {
            queryWriter.CreateTemporaryIdentityTable(Table);
            queryWriter.Insert(Table);
            queryWriter.OutputIdentitiesToTemporaryTable(Table);
            queryWriter.ValueEntities(Entities);
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
        private async Task AssignIdentity(ISqlQueryReader queryReader, ValueEntity<TEntity> valueEntity)
        {
            if (!await queryReader.Advance())
                throw new InvalidOperationException("There were less new identities than inserted entities");
            AssignIdentityDelegate(queryReader, valueEntity.Source);
        }
    }
}
