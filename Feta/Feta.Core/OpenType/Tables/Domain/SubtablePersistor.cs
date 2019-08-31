using System;
using System.Collections.Generic;
using System.Text;

namespace Feta.OpenType.Tables.Domain
{
    using Abstraction;
    using OpenType.Abstraction;
    using OpenType.Implementation;
    using OpenType.Domain;

    public abstract class SubtablePersistor<T, U> : ITablePersistor<U>
        where T : PrimaryTable
        where U : BaseTable
    {
        public void Read(IFontReader reader)
        {
            T parentTable = reader.Tables.Retrieve<T>();
            U subTable = ReadSubtable(reader);
            AttachSubtable(parentTable, subTable);
        }
        protected abstract U ReadSubtable(IFontReader reader);
        protected abstract void AttachSubtable(T parentTable, U subTable);

        public abstract void Write(IFontWriter writer, U table);
    }
}
