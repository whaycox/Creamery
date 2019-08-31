namespace Feta.OpenType.Tables.ClassDefinition.Mock
{
    using OpenType.Mock;

    public class Persistor : Persistor<PrimaryTable>
    {
        protected override void AttachSubtable(PrimaryTable parentTable, ClassDefinition.Table subTable) => parentTable.Table = subTable;
    }
}
