namespace Feta.OpenType.Tables.GDEF
{
    public class MarkAttachmentClassDefinitionPersistor : ClassDefinition.Persistor<Table>
    {
        protected override void AttachSubtable(Table parentTable, ClassDefinition.Table subTable) => parentTable.MarkAttachmentClassDefinition = subTable;
    }
}
