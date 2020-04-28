using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Curds.Persistence.Model.Implementation
{
    using Persistence.Abstraction;
    using Abstraction;
    using Query.Domain;
    using Persistence.Domain;
    using Domain;

    internal abstract class ModelMap
    { }

    internal class ModelMap<TModel> : ModelMap, IModelMap<TModel>
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

                AssignIdentityDelegate = AssignIdentityDelegatesByType[typeof(TEntity)],
                ValueEntityDelegate = ValueEntityDelegatesByType[typeof(TEntity)],
                ProjectEntityDelegate = ProjectEntityDelegatesByType[typeof(TEntity)] as ProjectEntityDelegate<TEntity>,
            };
    }
}
