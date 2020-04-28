using System;

namespace Curds.Persistence.Model.Domain
{
    using Curds.Persistence.Abstraction;
    using Curds.Persistence.Domain;
    using Model.Abstraction;

    public class NotAnInterfaceModel : IDataModel
    {
        public TestEntity PublicEntity { get; }

        public TEntity Entity<TEntity>()
            where TEntity : IEntity => throw new NotImplementedException();
    }
}
