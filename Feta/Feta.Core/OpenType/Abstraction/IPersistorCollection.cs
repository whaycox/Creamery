namespace Feta.OpenType.Abstraction
{
    public interface IPersistorCollection
    {
        ITablePersistor RetrievePersistor(string tag);
    }
}
