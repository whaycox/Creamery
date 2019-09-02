namespace Feta.OpenType.Implementation
{
    using Abstraction;
    using Domain;

    public abstract class SubtablePersistor<T, U> : BaseTablePersistor<U>
        where T : PrimaryTable
        where U : BaseTable
    {
        public override void Read(IFontReader reader)
        {
            T parentTable = reader.Tables.Retrieve<T>();
            U subTable = ReadSubtable(reader);
            AttachSubtable(parentTable, subTable);
        }
        protected abstract U ReadSubtable(IFontReader reader);
        protected abstract void AttachSubtable(T parentTable, U subTable);
    }
}
