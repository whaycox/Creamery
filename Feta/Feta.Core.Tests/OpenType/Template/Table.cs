using Curds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Feta.OpenType.Template
{
    using Mock;

    public abstract class Table<T> : Table
    {
        protected abstract T TestObject { get; }
    }

    public abstract class Table : Test
    {
        protected Mock.Table MockTable = new Mock.Table();
        protected PrimaryTable MockPrimaryTable = new PrimaryTable();

        protected ITableCollection MockTableCollection = new ITableCollection();
        protected IOffsetRegistry MockOffsetRegistry = new IOffsetRegistry();
        protected IFontReader MockReader = new IFontReader();
        protected IFontWriter MockWriter = new IFontWriter();

        [TestInitialize]
        public void SetReferences()
        {
            MockReader.Offsets = MockOffsetRegistry;
            MockReader.Tables = MockTableCollection;
        }
    }
}
