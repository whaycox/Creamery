using System;
using System.Collections.Generic;
using System.Text;
using Feta.OpenType.Abstraction;
using Feta.OpenType.Domain;

namespace Feta.OpenType.Mock
{
    public class IParsedTables : Abstraction.IParsedTables
    {
        public BaseTable this[Type index] => throw new NotImplementedException();

        public void ParseCurrentTable(FontReader reader)
        {
            throw new NotImplementedException();
        }

        public List<(uint offset, TableParseDelegate parseDelegate)> Registered = new List<(uint offset, TableParseDelegate parseDelegate)>();
        public void Register(uint offset, TableParseDelegate parseDelegate) => Registered.Add((offset, parseDelegate));

        public T Retrieve<T>() where T : BaseTable
        {
            throw new NotImplementedException();
        }

        public List<BaseTable> Added = new List<BaseTable>();
        public void Add<T>(T table) where T : BaseTable => Added.Add(table);
    }
}
