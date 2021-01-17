namespace Curds.Persistence.Abstraction
{
    public interface IKeyGenerator<TKey>
    {
        TKey Next { get; }
    }
}
