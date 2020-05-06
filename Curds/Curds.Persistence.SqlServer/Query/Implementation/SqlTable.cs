using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Curds.Persistence.Query.Implementation
{
    using Abstraction;
    using Model.Abstraction;
    using Persistence.Abstraction;
    using Domain;

    internal class SqlTable : ISqlTable
    {
        public IEntityModel Model { get; set; }
        public string Schema => Model.Schema;
        public string Name => Model.Table;

        public IList<ISqlColumn> Columns => Model.Values.Select(value => BuildColumn(value)).ToList();
        public IList<ISqlColumn> Keys => Model.Keys.Select(key => BuildColumn(key)).ToList();
        public ISqlColumn Identity => BuildColumn(Model.Identity);
        public IEnumerable<ISqlColumn> NonIdentities => Model.NonIdentities.Select(value => BuildColumn(value));

        public ValueEntity BuildValueEntity(IEntity entity) => Model.ValueEntity(entity);
        public void AssignIdentities(ISqlQueryReader reader, IEntity entity) => Model.AssignIdentity(reader, entity);
        public IEntity ProjectEntity(ISqlQueryReader reader) => Model.ProjectEntity(reader);

        private ISqlColumn BuildColumn(IValueModel valueModel) => new SqlColumn(this, valueModel);
    }
}
