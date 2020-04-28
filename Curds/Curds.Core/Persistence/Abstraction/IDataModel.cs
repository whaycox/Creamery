namespace Curds.Persistence.Abstraction
{
    public interface IDataModel
    {
        TEntity Entity<TEntity>() where TEntity : IEntity;
    }
}
