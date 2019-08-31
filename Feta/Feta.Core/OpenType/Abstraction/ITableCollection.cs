namespace Feta.OpenType.Abstraction
{
    using Domain;

    public interface ITableCollection
    {
        void Add<T>(T table) where T : BaseTable;
        T Retrieve<T>() where T : BaseTable;
        PrimaryTable Retrieve(string tag);
    }
}
