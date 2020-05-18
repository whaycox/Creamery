using System.Collections.Generic;
using System.Linq;
using System;

namespace Curds.Persistence.Query.Implementation
{
    using Abstraction;
    using Domain;
    using Model.Abstraction;
    using Persistence.Abstraction;

    internal class SqlTable : ISqlTable
    {
        public IEntityModel Model { get; set; }

        public Type EntityType => Model.EntityType;
        public string Schema => Model.Schema;
        public string Name => Model.Table;

        public IList<ISqlColumn> Columns => Model.Values.Select(value => BuildColumn(value)).ToList();
        public IList<ISqlColumn> Keys => Model.Keys.Select(key => BuildColumn(key)).ToList();
        public ISqlColumn KeyColumn => BuildColumn(Model.KeyValue);
        public ISqlColumn Identity => BuildColumn(Model.Identity);
        public IEnumerable<ISqlColumn> NonIdentities => Model.NonIdentities.Select(value => BuildColumn(value));

        public ValueEntity BuildValueEntity(IEntity entity) => Model.ValueEntity(entity);
        public void AssignIdentities(ISqlQueryReader reader, IEntity entity) => Model.AssignIdentity(reader, entity);
        public IEntity ProjectEntity(ISqlQueryReader reader) => Model.ProjectEntity(reader);

        private ISqlColumn BuildColumn(IValueModel valueModel) => new SqlColumn(this, valueModel);
    }
}
