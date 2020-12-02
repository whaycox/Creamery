namespace Curds.Clone.Abstraction
{
    public interface ICloneFactory
    {
        TEntity Clone<TEntity>(TEntity source) where TEntity : class;
    }
}
