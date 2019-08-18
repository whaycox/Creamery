namespace Feta.OpenType.Tables.Abstraction
{
    using Domain;
    using OpenType.Domain;
    using OpenType.Abstraction;

    public interface ITablePersistor<T> where T : BaseTable
    {
        IParsedTables Read(FontReader reader, IParsedTables parsedTables);
        void Write(FontWriter writer, T table);
    }
}
