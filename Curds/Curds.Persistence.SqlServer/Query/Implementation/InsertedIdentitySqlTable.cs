using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Query.Implementation
{
    using Abstraction;
    using Curds.Persistence.Abstraction;
    using Curds.Persistence.Query.Domain;

    internal class InsertedIdentitySqlTable : ISqlTable
    {
        private SqlTable Table { get; }

        public Type EntityType => Table.EntityType;
        public string Schema => string.Empty;
        public string Name => $"#{Table.Name}_Identities";
        public string Alias => Table.Alias;
        public List<ISqlColumn> Columns => new List<ISqlColumn> { Table.Identity };
        public List<ISqlColumn> Keys => Table.Keys;
        public ISqlColumn KeyColumn => Table.KeyColumn;
        public ISqlColumn Identity => Table.Identity;
        public IEnumerable<ISqlColumn> NonIdentities => Table.NonIdentities;
        public ISqlTable InsertedIdentityTable => this;

        public InsertedIdentitySqlTable(SqlTable table)
        {
            Table = table;
        }

        public ValueEntity BuildValueEntity(IEntity entity) => Table.BuildValueEntity(entity);
        public void AssignIdentities(ISqlQueryReader reader, IEntity entity) => Table.AssignIdentities(reader, entity);
        public IEntity ProjectEntity(ISqlQueryReader reader) => Table.ProjectEntity(reader);
    }
}
