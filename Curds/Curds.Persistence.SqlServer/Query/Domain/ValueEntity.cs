using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Query.Domain
{
    using Persistence.Abstraction;

    public abstract class ValueEntity
    {
        public List<Value> Values { get; set; } = new List<Value>();
    }

    public class ValueEntity<TEntity> : ValueEntity
        where TEntity : IEntity
    {
        public TEntity Source { get; set; }
    }
}
