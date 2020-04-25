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

        public ModelMap(IModelBuilder modelBuilder)
        {
            TablesByType = modelBuilder.TablesByType<TModel>();
            ValueEntityDelegatesByType = modelBuilder.ValueEntityDelegatesByType<TModel>();
            AssignIdentityDelegatesByType = modelBuilder.AssignIdentityDelegatesByType<TModel>();
        }

        public Table Table(Type type) => TablesByType[type];

        public ValueEntity<TEntity> ValueEntity<TEntity>(TEntity entity)
            where TEntity : IEntity => ValueEntityDelegatesByType[typeof(TEntity)](entity) as ValueEntity<TEntity>;

        public AssignIdentityDelegate AssignIdentityDelegate<TEntity>()
            where TEntity : IEntity => AssignIdentityDelegatesByType[typeof(TEntity)];
    }
}
