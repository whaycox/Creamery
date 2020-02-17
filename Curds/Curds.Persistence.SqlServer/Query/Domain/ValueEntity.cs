using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Query.Domain
{
    using Persistence.Domain;

    public abstract class ValueEntity
    {
        public List<Value> Values { get; set; } = new List<Value>();
    }

    public class ValueEntity<TEntity> : ValueEntity
        where TEntity : BaseEntity
    {
        public TEntity Source { get; set; }
    }
}
