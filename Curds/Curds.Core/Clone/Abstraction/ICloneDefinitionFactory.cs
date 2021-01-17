namespace Curds.Clone.Abstraction
{
    public interface ICloneDefinitionFactory
    {
        ICloneDefinition<TEntity> Create<TEntity>(ICloneFactory cloneFactory) where TEntity : class;
    }
}
