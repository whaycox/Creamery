namespace Curds.Collections.Abstraction
{
    public interface IMonoLinkNode<TEntity>
    {
        IMonoLinkNode<TEntity> Next { get; }

        TEntity Value { get; }
    }
}
