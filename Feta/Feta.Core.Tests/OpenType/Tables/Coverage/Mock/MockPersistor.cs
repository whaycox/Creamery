namespace Feta.OpenType.Tables.Coverage.Mock
{
    using OpenType.Mock;
    using Implementation;
    using Domain;

    public class MockPersistor : CoveragePersistor<MockPrimaryTable>
    {
        protected override void AttachSubtable(MockPrimaryTable parentTable, CoverageTable subTable) => parentTable.Table = subTable;
    }
}
