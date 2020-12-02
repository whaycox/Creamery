using System;
using System.Collections.Generic;

namespace Curds.Clone.Implementation
{
    using Abstraction;

    internal class CloneFactory : ICloneFactory
    {
        private ICloneDefinitionFactory DefinitionFactory { get; }

        private object Locker { get; } = new object();
        private Dictionary<Type, ICloneDefinition> Definitions { get; } = new Dictionary<Type, ICloneDefinition>();

        public CloneFactory(ICloneDefinitionFactory definitionFactory)
        {
            DefinitionFactory = definitionFactory;
        }

        public TEntity Clone<TEntity>(TEntity source)
            where TEntity : class
        {
            if (!Definitions.ContainsKey(typeof(TEntity)))
                AddNewDefinition<TEntity>();
            ICloneDefinition<TEntity> definition = Definitions[typeof(TEntity)] as ICloneDefinition<TEntity>;
            return definition.Clone(source);
        }
        private void AddNewDefinition<TEntity>()
            where TEntity : class
        {
            lock (Locker)
            {
                if (!Definitions.ContainsKey(typeof(TEntity)))
                {
                    Definitions.Add(typeof(TEntity), DefinitionFactory.Create<TEntity>(this));
                }
            }
        }
    }
}
