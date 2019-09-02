namespace Feta.OpenType.Abstraction
{
    using Domain;

    public interface ITablePersistor
    {
        void Read(IFontReader reader);
        void Write(IFontWriter writer, BaseTable table);
    }
}
