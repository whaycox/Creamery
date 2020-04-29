using System;
using System.Collections.Generic;

namespace Curds.Persistence.Model.Implementation
{
    using Abstraction;
    using Domain;
    using Persistence.Abstraction;

    internal class ModelMap<TModel> : IModelMap<TModel>
        where TModel : IDataModel
    {
        private Dictionary<Type, Table> TablesByType { get; }
        private Dictionary<Type, ValueEntityDelegate> ValueEntityDelegatesByType { get; }
        private Dictionary<Type, AssignIdentityDelegate> AssignIdentityDelegatesByType { get; }
        private Dictionary<Type, ProjectEntityDelegate<IEntity>> ProjectEntityDelegatesByType { get; }

        public ModelMap(IModelBuilder modelBuilder)
        {
            TablesByType = modelBuilder.TablesByType<TModel>();
            ValueEntityDelegatesByType = modelBuilder.ValueEntityDelegatesByType<TModel>();
            AssignIdentityDelegatesByType = modelBuilder.AssignIdentityDelegatesByType<TModel>();
            ProjectEntityDelegatesByType = modelBuilder.ProjectEntityDelegatesByType<TModel>();
        }

        public IEntityModel<TEntity> Entity<TEntity>()
            where TEntity : IEntity => new EntityModel<TEntity>
            {
                _table = TablesByType[typeof(TEntity)],

                AssignIdentity = AssignIdentityDelegatesByType[typeof(TEntity)],
                ValueEntity = ValueEntityDelegatesByType[typeof(TEntity)],
                ProjectEntity = ProjectEntityDelegatesByType[typeof(TEntity)] as ProjectEntityDelegate<TEntity>,
            };
    }
}
