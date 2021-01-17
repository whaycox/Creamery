namespace Curds.Clone.Abstraction
{
    public delegate TEntity CloneDelegate<TEntity>(TEntity source, ICloneFactory cloneFactory);
}
