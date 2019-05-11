using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Persistor.Mock
{
    public abstract class ProtoMock<T> where T : Domain.BaseEntity
    {
        protected abstract List<T> Samples { get; }
    }
}
