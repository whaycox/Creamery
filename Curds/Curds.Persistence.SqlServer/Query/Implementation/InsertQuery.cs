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
        public ValueEntity Entity { get; set; }

        public void Write(ISqlQueryWriter queryWriter)
        {
            queryWriter.Insert(Table);
            queryWriter.ValueEntities(new List<ValueEntity> { Entity });
        }
    }
}
