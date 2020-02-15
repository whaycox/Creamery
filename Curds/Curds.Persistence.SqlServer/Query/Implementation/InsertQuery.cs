using System.Collections.Generic;

namespace Curds.Persistence.Query.Implementation
{
    using Abstraction;
    using Domain;
    using Persistence.Domain;
    using Model.Domain;

    public class InsertQuery<TEntity> : ISqlQuery
        where TEntity : BaseEntity
    {
        public Table Table { get; set; }
        public List<ValueEntity> Entities { get; set; } = new List<ValueEntity>();

        public void Write(ISqlQueryWriter queryWriter)
        {
            queryWriter.Insert(Table);
            queryWriter.ValueEntities(Entities);
        }
    }
}
