namespace Curds.Clone.Abstraction
{
    public interface ICloneDefinition
    { }

    public interface ICloneDefinition<TEntity> : ICloneDefinition
    {
        TEntity Clone(TEntity source);
    }
}
