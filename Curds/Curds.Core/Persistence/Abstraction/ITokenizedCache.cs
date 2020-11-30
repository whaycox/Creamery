namespace Curds.Persistence.Abstraction
{
    public interface ITokenizedCache<TEntity>
    {
        string Store(TEntity entity);
        TEntity Consume(string token);
    }
}
