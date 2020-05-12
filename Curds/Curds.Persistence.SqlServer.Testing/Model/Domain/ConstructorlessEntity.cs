using System;

namespace Curds.Persistence.Model.Domain
{
    using Persistence.Abstraction;

    public class ConstructorlessEntity : IEntity
    {
        private ConstructorlessEntity()
        { }

        public object[] Keys => throw new NotImplementedException();
    }
}
