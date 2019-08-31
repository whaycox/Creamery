using System;
using System.Collections.Generic;
using System.Linq;

namespace Feta.OpenType.Mock
{
    using OpenType.Abstraction;
    using OpenType.Domain;
    using OpenType.Implementation;

    public class ITableCollection : Abstraction.ITableCollection
    {
        public void ParseCurrentTable(FontReader reader)
        {
            throw new NotImplementedException();
        }

        public List<(uint offset, TableParseDelegate parseDelegate)> Registered = new List<(uint offset, TableParseDelegate parseDelegate)>();
        public void Register(uint offset, TableParseDelegate parseDelegate) => Registered.Add((offset, parseDelegate));

        public BaseTable TableToRetrieve = null;
        public T Retrieve<T>() where T : BaseTable => TableToRetrieve as T;

        public List<BaseTable> TablesAdded = new List<BaseTable>();
        public void Add<T>(T table) where T : BaseTable => TablesAdded.Add(table);

        public Domain.PrimaryTable PrimaryTableToRetrieve = null;
        public List<string> TagsRetrieved = new List<string>();
        public Domain.PrimaryTable Retrieve(string tag)
        {
            TagsRetrieved.Add(tag);
            return PrimaryTableToRetrieve;
        }
    }
}
