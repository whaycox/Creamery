namespace Curds.Persistence.Abstraction
{
    public interface IDataModel
    {
        ITable<TEntity> Table<TEntity>() 
            where TEntity : IEntity;
    }
}
