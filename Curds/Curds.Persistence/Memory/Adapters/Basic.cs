using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain.Persistence;

namespace Curds.Persistence.Memory.Adapters
{
    public abstract class Basic<T> where T : Entity
    {
        private Dictionary<int, T> Storage = new Dictionary<int, T>();

        public T Insert(T newEntity)
        {
            if (newEntity == null)
                throw new ArgumentNullException(nameof(newEntity));

            throw new NotImplementedException();
        }


    }
}
