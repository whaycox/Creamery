using Curds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Feta.OpenType.Template
{
    using Mock;

    public abstract class TableTemplate<T> : TableTemplate
    {
        protected abstract T TestObject { get; }
    }

    public abstract class TableTemplate : Test
    {
        protected MockTable MockTable = new MockTable();
        protected MockPrimaryTable MockPrimaryTable = new MockPrimaryTable();

        protected MockTablePersistor MockTablePersistor = new MockTablePersistor();
        protected MockPersistorCollection MockPersistorCollection = new MockPersistorCollection();
        protected MockTableCollection MockTableCollection = new MockTableCollection();
        protected MockOffsetRegistry MockOffsetRegistry = new MockOffsetRegistry();
        protected MockFontReader MockReader = new MockFontReader();
        protected MockFontWriter MockWriter = new MockFontWriter();

        [TestInitialize]
        public void SetReferences()
        {
            MockPersistorCollection.PersistorToReturn = MockTablePersistor;
            MockReader.Offsets = MockOffsetRegistry;
            MockReader.Tables = MockTableCollection;
        }
    }
}
