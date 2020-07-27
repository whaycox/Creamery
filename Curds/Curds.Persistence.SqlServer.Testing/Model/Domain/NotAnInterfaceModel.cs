using System;

namespace Curds.Persistence.Model.Domain
{
    using Persistence.Abstraction;
    using Persistence.Domain;

    public class NotAnInterfaceModel : IDataModel
    {
        public TestEntity PublicEntity { get; }

        public TEntity Entity<TEntity>()
            where TEntity : IEntity => throw new NotImplementedException();
    }
}
