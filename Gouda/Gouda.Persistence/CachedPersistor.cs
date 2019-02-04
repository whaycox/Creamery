using System;
using System.Collections.Generic;
using System.Text;
using Curds.Application.Persistence;
using Curds.Domain.Persistence;
using Curds.Persistence;

namespace Gouda.Persistence
{
    public class CachedPersistor<T> : IPersistor<T> where T : Entity
    {
        private Cache<T> Cache = new Cache<T>();

        public CachedPersistor(IEnumerable<T> startingSet)
        {
            Cache.AddOrUpdate(startingSet);
        }

        public IEnumerable<T> FetchAll() => Cache.RetrieveAll();

        public T Lookup(int id) => Cache.Retrieve(id);
    }
}
