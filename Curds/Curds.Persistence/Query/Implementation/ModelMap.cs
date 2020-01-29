using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Query.Implementation
{
    using Persistence.Domain;
    using Abstraction;
    using Persistence.Abstraction;
    using Domain;

    internal class ModelMap<TModel> : IModelMap<TModel>
        where TModel : IDataModel
    {
        public Dictionary<string, Table> TablesByName { get; }
        public Dictionary<Type, Table> TablesByType { get; }
        public Dictionary<Type, ValueEntityDelegate> ValueEntityBuilders { get; }

        public ModelMap(IModelMapper typeMapper)
        {
            TablesByName = typeMapper.MapTablesByName<TModel>();
            TablesByType = typeMapper.MapTablesByType<TModel>();
            ValueEntityBuilders = typeMapper.MapValueEntityDelegates<TModel>();
        }

        public Table Table(string name) => TablesByName[name];
        public Table Table(Type type) => TablesByType[type];

        public ValueEntity ValueEntity<TEntity>(TEntity entity) where TEntity : BaseEntity => 
            ValueEntityBuilders[typeof(TEntity)](entity);
    }
}
