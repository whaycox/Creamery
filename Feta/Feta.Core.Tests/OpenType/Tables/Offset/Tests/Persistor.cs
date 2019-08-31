using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Feta.OpenType.Tables.Offset.Tests
{
    using Exceptions;
    using Tables.Mock;

    [TestClass]
    public class Persistor : Template.ITablePersistor<Offset.Persistor, Table>
    {
        private static IEnumerable<object[]> SampleData => Mock.Table.DynamicData;

        private IPersistorCollection MockPersistorCollection = new IPersistorCollection();

        private Offset.Persistor _obj = null;
        protected override Offset.Persistor TestObject => _obj;

        protected override void PrimeTableToRead(OpenType.Mock.IFontReader mockReader, Table table)
        {
            throw new System.NotImplementedException();

        }
        protected override void VerifyTablesAreEqual(Table expected, Table actual)
        {
            throw new System.NotImplementedException();
        }

        protected override void VerifyTableWasWritten(OpenType.Mock.IFontWriter mockWriter, Table table)
        {
            throw new System.NotImplementedException();
        }

        [TestInitialize]
        public void BuildObj()
        {
            _obj = new Offset.Persistor(MockPersistorCollection);
        }

        [DataTestMethod]
        [DynamicData(nameof(SampleData), DynamicDataSourceType.Property)]
        public void ReadAddsTable(Table sample)
        {
            Mock.Table.PrimeTableToRead(MockReader, sample);
            TestObject.Read(MockReader);
            Assert.AreEqual(1, MockTableCollection.TablesAdded.Count);
        }

        [DataTestMethod]
        [DynamicData(nameof(SampleData), DynamicDataSourceType.Property)]
        public void AddedTableIsExpected(Table sample)
        {
            Mock.Table.PrimeTableToRead(MockReader, sample);
            TestObject.Read(MockReader);
            Mock.Table.VerifyTablesAreEqual(sample, MockTableCollection.TablesAdded[0] as Table);
        }

        [DataTestMethod]
        [DynamicData(nameof(SampleData), DynamicDataSourceType.Property)]
        public void ReadAddsOffsetsToRegistry(Table sample)
        {
            Mock.Table.PrimeTableToRead(MockReader, sample);
            TestObject.Read(MockReader);
            Assert.AreEqual(sample.Records.Count, MockOffsetRegistry.RegisteredParsers.Count);

            for (int i = 0; i < sample.Records.Count; i++)
                Assert.AreEqual(sample.Records[i].Offset, MockOffsetRegistry.RegisteredParsers[i].offset);
        }

        [DataTestMethod]
        [DynamicData(nameof(SampleData), DynamicDataSourceType.Property)]
        public void ReadRequestsRecordByTag(Table sample)
        {
            Mock.Table.PrimeTableToRead(MockReader, sample);
            TestObject.Read(MockReader);
            Assert.AreEqual(sample.Records.Count, MockPersistorCollection.TagsRetrieved.Count);

            for (int i = 0; i < sample.Records.Count; i++)
                Assert.AreEqual(sample.Records[i].Tag, MockPersistorCollection.TagsRetrieved[i]);
        }

        [TestMethod]
        [ExpectedException(typeof(MisorderedTagsException))]
        public void ReadThrowsIfTagsAreMisordered()
        {
            Mock.Table.PrimeTableToRead(MockReader, Mock.Table.Misordered);
            TestObject.Read(MockReader);
        }

        [DataTestMethod]
        [DynamicData(nameof(SampleData), DynamicDataSourceType.Property)]
        public void WritesCorrectData(Table sample)
        {
            TestObject.Write(MockWriter, sample);
            Mock.Table.VerifyTableWasWritten(MockWriter, sample);
        }

        [DataTestMethod]
        [DynamicData(nameof(SampleData), DynamicDataSourceType.Property)]
        public void WriteDefersOffsetAndLength(Table sample)
        {
            TestObject.Write(MockWriter, sample);
            Assert.AreEqual(sample.Records.Count * 2, MockWriter.DeferredUInt32s.Count);
        }

        private uint ExecuteFirstDeferredWrite(int record) => MockWriter.DeferredUInt32s[(record * 2)](MockTableCollection, MockOffsetRegistry);
        private uint ExecuteSecondDeferredWrite(int record) => MockWriter.DeferredUInt32s[(record * 2) + 1](MockTableCollection, MockOffsetRegistry);

        [DataTestMethod]
        [DynamicData(nameof(SampleData), DynamicDataSourceType.Property)]
        public void FirstDeferredWriteRetrievesTableByTag(Table sample)
        {
            TestObject.Write(MockWriter, sample);
            for (int i = 0; i < sample.Records.Count; i++)
            {
                ExecuteFirstDeferredWrite(i);
                TableRecord expectedRecord = sample.Records[i];
                Assert.AreEqual(i + 1, MockTableCollection.TagsRetrieved.Count);
                Assert.AreEqual(expectedRecord.Tag, MockTableCollection.TagsRetrieved[i]);
            }
        }

        [DataTestMethod]
        [DynamicData(nameof(SampleData), DynamicDataSourceType.Property)]
        public void FirstDeferredWriteRetrievesOffsetWithPrimaryTableKey(Table sample)
        {
            TestObject.Write(MockWriter, sample);
            for (int i = 0; i < sample.Records.Count; i++)
            {
                MockTableCollection.PrimaryTableToRetrieve = MockPrimaryTable;
                ExecuteFirstDeferredWrite(i);
                Assert.AreEqual(i + 1, MockOffsetRegistry.OffsetKeysRetrieved.Count);
                Assert.AreSame(MockPrimaryTable, MockOffsetRegistry.OffsetKeysRetrieved[i]);
            }
        }

        [DataTestMethod]
        [DynamicData(nameof(SampleData), DynamicDataSourceType.Property)]
        public void FirstDeferredWriteReturnsOffsetFromKey(Table sample)
        {
            TestObject.Write(MockWriter, sample);
            for (int i = 0; i < sample.Records.Count; i++)
            {
                uint expected = (uint)i * 10;
                MockOffsetRegistry.OffsetToRetrieve = expected;
                Assert.AreEqual(expected, ExecuteFirstDeferredWrite(i));
            }
        }

        [DataTestMethod]
        [DynamicData(nameof(SampleData), DynamicDataSourceType.Property)]
        public void SecondDeferredWriteRetrievesTableByTag(Table sample)
        {
            TestObject.Write(MockWriter, sample);
            for (int i = 0; i < sample.Records.Count; i++)
            {
                ExecuteSecondDeferredWrite(i);
                TableRecord expectedRecord = sample.Records[i];
                Assert.AreEqual(i + 1, MockTableCollection.TagsRetrieved.Count);
                Assert.AreEqual(expectedRecord.Tag, MockTableCollection.TagsRetrieved[i]);
            }
        }

        [DataTestMethod]
        [DynamicData(nameof(SampleData), DynamicDataSourceType.Property)]
        public void SecondDeferredWriteRetrievesLengthWithPrimaryTableKey(Table sample)
        {
            TestObject.Write(MockWriter, sample);
            for (int i = 0; i < sample.Records.Count; i++)
            {
                MockTableCollection.PrimaryTableToRetrieve = MockPrimaryTable;
                ExecuteSecondDeferredWrite(i);
                Assert.AreEqual(i + 1, MockOffsetRegistry.LengthKeysRetrieved.Count);
                Assert.AreSame(MockPrimaryTable, MockOffsetRegistry.LengthKeysRetrieved[i]);
            }
        }

        [DataTestMethod]
        [DynamicData(nameof(SampleData), DynamicDataSourceType.Property)]
        public void SecondDeferredWriteReturnsLengthFromKey(Table sample)
        {
            TestObject.Write(MockWriter, sample);
            for (int i = 0; i < sample.Records.Count; i++)
            {
                uint expected = (uint)i * 10;
                MockOffsetRegistry.LengthToRetrieve = expected;
                Assert.AreEqual(expected, ExecuteSecondDeferredWrite(i));
            }
        }
    }
}
