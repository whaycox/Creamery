using System;

namespace Curds.Persistence.Model.Domain
{
    using Curds.Persistence.Abstraction;
    using Curds.Persistence.Domain;

    public class NotAnInterfaceModel : IDataModel
    {
        public ITable<TestEntity> PublicEntity { get; }

        public ITable<TEntity> Table<TEntity>()
            where TEntity : IEntity => throw new NotImplementedException();
    }
}
