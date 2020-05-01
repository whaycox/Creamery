namespace Curds.Persistence.Query.Implementation
{
    using Abstraction;
    using Model.Abstraction;
    using Persistence.Abstraction;

    internal class SqlUniverse<TEntity> : ISqlUniverse<TEntity>
        where TEntity : IEntity
    {
        public IEntityModel Model { get; set; }

        public ISqlQuery<TEntity> ProjectEntity() => new ProjectEntityQuery<TEntity> { Model = Model };
    }
}
