using System.Collections.Generic;

namespace Feta.OpenType.Mock
{
    using Abstraction;
    using Feta.OpenType.Domain;

    public class MockTablePersistor : ITablePersistor
    {
        public List<IFontReader> ReadsMade = new List<IFontReader>();
        public void Read(IFontReader reader) => ReadsMade.Add(reader);

        public List<(IFontWriter writer, BaseTable table)> WritesMade = new List<(IFontWriter writer, BaseTable table)>();
        public void Write(IFontWriter writer, BaseTable table) => WritesMade.Add((writer, table));
    }
}
