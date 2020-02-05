using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Query.Implementation
{
    using Persistence.Abstraction;
    using Persistence.Domain;
    using Domain;

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
