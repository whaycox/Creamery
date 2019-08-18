namespace Feta.OpenType.Tables.Abstraction
{
    using OpenType.Abstraction;

    public interface IPersistorCollection
    {
        TableParseDelegate RetrieveParser(string tag);
    }
}
