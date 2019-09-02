namespace Feta.OpenType.Tables.ClassDefinition.Mock
{
    using OpenType.Mock;
    using Implementation;
    using Domain;

    public class MockClassDefinitionPersistor : ClassDefinitionPersistor<MockPrimaryTable>
    {
        protected override void AttachSubtable(MockPrimaryTable parentTable, ClassDefinitionTable subTable) => parentTable.Table = subTable;
    }
}
