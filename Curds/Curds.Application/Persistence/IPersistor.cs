using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain.Persistence;

namespace Curds.Application.Persistence
{
    public interface IPersistor<T> where T : Entity
    {
        IEnumerable<T> FetchAll();
        T Lookup(int id);
        IEnumerable<T> Lookup(IEnumerable<int> ids);

        void Update(int id, Func<T, T> updateDelegate);
    }
}
