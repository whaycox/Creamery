namespace Curds.Persistence.Abstraction
{
    using Domain;

    public interface ITable<TEntity>
        where TEntity : BaseEntity
    { }
}
