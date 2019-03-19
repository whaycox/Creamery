using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Domain.EventArgs
{
    using Persistence;

    public class EntityModifiedArgs<T> : System.EventArgs where T : Entity
    {
        public T Entity { get; }

        public EntityModifiedArgs(T entity)
        {
            Entity = entity;
        }
    }
}
