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
        private Dictionary<string, Table> TablesByName { get; }
        private Dictionary<Type, Table> TablesByType { get; }
        private Dictionary<Type, ValueEntityDelegate> ValueEntityDelegatesByType { get; }

        public ModelMap(IModelBuilder modelBuilder)
        {
            TablesByName = modelBuilder.TablesByName<TModel>();
            TablesByType = modelBuilder.TablesByType<TModel>();
            ValueEntityDelegatesByType = modelBuilder.ValueEntityDelegatesByType<TModel>();
        }

        public Table Table(string name) => TablesByName[name];
        public Table Table(Type type) => TablesByType[type];

        public ValueEntity ValueEntity<TEntity>(TEntity entity)
            where TEntity : BaseEntity => 
            ValueEntityDelegatesByType[typeof(TEntity)](entity);
    }
}
