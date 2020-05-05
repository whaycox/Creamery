namespace Curds.Persistence.Model.Abstraction
{
    using Persistence.Abstraction;

    public interface IModelMap
    {
        IEntityModel Entity<TEntity>()
            where TEntity : IEntity;
    }

    public interface IModelMap<TModel> : IModelMap
        where TModel : IDataModel
    { }
}
