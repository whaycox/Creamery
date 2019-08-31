namespace Feta.OpenType.Tables.Coverage.Mock
{
    using OpenType.Mock;

    public class Persistor : Persistor<PrimaryTable>
    {
        protected override void AttachSubtable(PrimaryTable parentTable, Coverage.Table subTable) => parentTable.Table = subTable;
    }
}
