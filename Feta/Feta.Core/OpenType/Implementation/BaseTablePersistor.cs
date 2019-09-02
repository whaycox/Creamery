namespace Feta.OpenType.Implementation
{
    using Abstraction;
    using Domain;

    public abstract class BaseTablePersistor : ITablePersistor
    {
        public abstract void Read(IFontReader reader);
        public abstract void Write(IFontWriter writer, BaseTable table);
    }

    public abstract class BaseTablePersistor<T> : BaseTablePersistor
        where T : BaseTable
    {
        public override void Write(IFontWriter writer, BaseTable table) => Write(writer, table as T);
        protected abstract void Write(IFontWriter writer, T table);
    }
}
