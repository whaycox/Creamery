using System;
using System.Collections.Generic;

namespace Feta.OpenType.Domain
{
    using Abstraction;

    public class ParsedTables : IParsedTables
    {
        private Dictionary<Type, BaseTable> Tables { get; } = new Dictionary<Type, BaseTable>();

        public BaseTable this[Type index]
        {
            get
            {
                if (!Tables.ContainsKey(index))
                    return null;
                else
                    return Tables[index];
            }
        }

        public T Retrieve<T>() where T : BaseTable => this[typeof(T)] as T;
        public void Add<T>(T table) where T : BaseTable
        {
            if (Tables.ContainsKey(typeof(T)))
                throw new InvalidOperationException($"{nameof(ParsedTables)} already has an entry for {typeof(T).FullName}");
            Tables.Add(typeof(T), table);
        }

        public void Register(uint offset, TableParseDelegate parseDelegate)
        {
            throw new NotImplementedException();
        }

        public void ParseCurrentTable(FontReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
