namespace Curds.Clone.Implementation
{
    using Abstraction;

    internal class CloneDefinition<TEntity> : ICloneDefinition<TEntity>
    {
        private ICloneFactory CloneFactory { get; }
        private CloneDelegate<TEntity> CloneDelegate { get; }

        public CloneDefinition(
            ICloneFactory cloneFactory,
            CloneDelegate<TEntity> cloneDelegate)
        {
            CloneFactory = cloneFactory;
            CloneDelegate = cloneDelegate;
        }

        public TEntity Clone(TEntity source) => CloneDelegate(source, CloneFactory);
    }
}
