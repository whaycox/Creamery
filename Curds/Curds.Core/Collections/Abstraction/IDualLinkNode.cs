namespace Curds.Collections.Abstraction
{
    public interface IDualLinkNode<TEntity>
    {
        IDualLinkNode<TEntity> Previous { get; }
        IDualLinkNode<TEntity> Next { get; }

        TEntity Value { get; }
    }
}
