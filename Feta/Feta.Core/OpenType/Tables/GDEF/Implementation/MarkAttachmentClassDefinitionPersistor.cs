namespace Feta.OpenType.Tables.GDEF.Implementation
{
    using ClassDefinition.Domain;
    using ClassDefinition.Implementation;
    using Domain;

    public class MarkAttachmentClassDefinitionPersistor : ClassDefinitionPersistor<GdefTable>
    {
        protected override void AttachSubtable(GdefTable parentTable, ClassDefinitionTable subTable) => parentTable.MarkAttachmentClassDefinition = subTable;
    }
}
