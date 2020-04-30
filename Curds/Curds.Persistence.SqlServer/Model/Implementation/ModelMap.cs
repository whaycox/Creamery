using System;
using System.Collections.Generic;
using System.Linq;

namespace Curds.Persistence.Model.Implementation
{
    using Abstraction;
    using Domain;
    using Persistence.Abstraction;

    internal class ModelMap<TModel> : IModelMap<TModel>
        where TModel : IDataModel
    {
        private Dictionary<Type, IEntityModel> EntityModels { get; }

        public ModelMap(IModelBuilder modelBuilder)
        {
            EntityModels = modelBuilder
                .BuildEntityModels<TModel>()
                .ToDictionary(key => key.EntityType);
        }

        public IEntityModel Entity<TEntity>()
            where TEntity : IEntity => EntityModels[typeof(TEntity)];
    }
}
