using System;
using System.Collections.Generic;

namespace Feta.OpenType.Implementation
{
    using Abstraction;
    using Domain;

    public class TableCollection : ITableCollection
    {
        private Dictionary<Type, BaseTable> Tables { get; } = new Dictionary<Type, BaseTable>();
        private Dictionary<string, PrimaryTable> PrimaryTables { get; } = new Dictionary<string, PrimaryTable>();

        public void Add<T>(T table) where T : BaseTable
        {
            if (table == null)
                throw new ArgumentNullException(nameof(table));
            if (Tables.ContainsKey(typeof(T)))
                throw new ArgumentException($"{nameof(TableCollection)} already has an entry for {typeof(T).FullName}");

            AddPrimaryTableIfNecessary(table);
            Tables.Add(typeof(T), table);
        }
        private void AddPrimaryTableIfNecessary(BaseTable table)
        {
            if (table is PrimaryTable)
            {
                PrimaryTable primaryTable = table as PrimaryTable;
                PrimaryTables.Add(primaryTable.Tag, primaryTable);
            }
        }
        public T Retrieve<T>() where T : BaseTable
        {
            if (!Tables.TryGetValue(typeof(T), out BaseTable toReturn))
                throw new InvalidOperationException("Cannot retrieve a table prior to adding it");
            return toReturn as T;
        }

        public PrimaryTable Retrieve(string tag)
        {
            if (!PrimaryTables.TryGetValue(tag, out PrimaryTable toReturn))
                throw new InvalidOperationException("Cannot retrieve a primary table prior to adding it");
            return toReturn;
        }
    }
}
