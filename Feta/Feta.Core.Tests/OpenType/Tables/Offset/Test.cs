using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;

namespace Feta.OpenType.Tables.Offset
{
    using Tables.Mock;

    [TestClass]
    public class Test : Template.ITablePersistor<Persistor, Table>
    {
        private IPersistorCollection MockPersistorCollection = new IPersistorCollection();

        private Persistor _obj = null;
        protected override Persistor TestObject => _obj;

        protected override Table[] Samples => Mock.Samples;
        protected override void VerifyTablesAreEqual(Table expected, Table actual) => Mock.VerifyEqual(expected, actual);

        [TestInitialize]
        public void BuildObj()
        {
            _obj = new Persistor(MockPersistorCollection);
        }

        [TestMethod]
        public void AddsOffsetsToRegistry()
        {
            var testTable = Mock.One;
            TestObject.Write(Writer, testTable);
            TestStream.Seek(0, SeekOrigin.Begin);

            TestObject.Read(Reader, MockParsedTables);
            Assert.AreEqual(testTable.NumberOfTables, MockParsedTables.Registered.Count);

            int currentIndex = 0;
            foreach (var table in testTable.Records.OrderBy(t => t.Tag))
                Assert.AreEqual(table.Offset, MockParsedTables.Registered[currentIndex++].offset);
        }
    }
}
