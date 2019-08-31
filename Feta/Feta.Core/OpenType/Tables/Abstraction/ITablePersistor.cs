namespace Feta.OpenType.Tables.Abstraction
{
    using OpenType.Abstraction;
    using OpenType.Domain;

    public interface ITablePersistor<T>
        where T : BaseTable
    {
        void Read(IFontReader reader);
        void Write(IFontWriter writer, T table);
    }
}
