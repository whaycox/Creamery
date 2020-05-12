using System;

namespace Curds.Persistence.Model.Domain
{
    using Persistence.Abstraction;

    public class NoEmptyConstructorEntity : IEntity
    {
        public NoEmptyConstructorEntity(int arg)
        { }

        public object[] Keys => throw new NotImplementedException();
    }
}
