using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Feta.OpenType.Tables.Offset.Tests
{
    using Exceptions;
    using Mock;
    using Implementation;
    using Domain;
    using Template;
    using OpenType.Mock;

    [TestClass]
    public class PersistorTest : ITablePersistorTemplate<OffsetPersistor, OffsetTable>
    {
        private static IEnumerable<object[]> SampleData => Mock.MockTable.DynamicData;

        private OffsetPersistor _obj = null;
        protected override OffsetPersistor TestObject => _obj;

        protected override void PrimeTableToRead(MockFontReader mockReader, OffsetTable table)
        {
            mockReader.PreparedUInt32s.Enqueue(table.SfntVersion);
            mockReader.PreparedUInt16s.Enqueue(table.NumberOfTables);
            mockReader.PreparedUInt16s.Enqueue(table.SearchRange);
            mockReader.PreparedUInt16s.Enqueue(table.EntrySelector);
            mockReader.PreparedUInt16s.Enqueue(table.RangeShift);

            foreach (TableRecord record in table.Records)
                PrimeTableRecord(mockReader, record);
        }
        private void PrimeTableRecord(MockFontReader mockReader, TableRecord record)
        {
            mockReader.PreparedTags.Enqueue(record.Tag);
            mockReader.PreparedUInt32s.Enqueue(record.Checksum);
            mockReader.PreparedUInt32s.Enqueue(record.Offset);
            mockReader.PreparedUInt32s.Enqueue(record.Length);
        }

        protected override void VerifyTablesAreEqual(OffsetTable expected, OffsetTable actual)
        {
            Assert.AreEqual(expected.SfntVersion, actual.SfntVersion);
            Assert.AreEqual(expected.NumberOfTables, actual.NumberOfTables);
            Assert.AreEqual(expected.RangeShift, actual.RangeShift);
            Assert.AreEqual(expected.SearchRange, actual.SearchRange);
            Assert.AreEqual(expected.EntrySelector, actual.EntrySelector);

            Assert.AreEqual(expected.Records.Count, actual.Records.Count);
            int currentIndex = 0;
            foreach (TableRecord expectedRecord in expected.Records)
            {
                TableRecord actualRecord = actual.Records[currentIndex++];

                Assert.AreEqual(expectedRecord.Tag, actualRecord.Tag);
                Assert.AreEqual(expectedRecord.Checksum, actualRecord.Checksum);
                Assert.AreEqual(expectedRecord.Length, actualRecord.Length);
                Assert.AreEqual(expectedRecord.Offset, actualRecord.Offset);
            }
        }

        protected override void VerifyTableWasWritten(MockFontWriter mockWriter, OffsetTable expected)
        {
            Assert.AreEqual(expected.SfntVersion, mockWriter.WrittenObjects[0]);
            Assert.AreEqual(expected.NumberOfTables, mockWriter.WrittenObjects[1]);
            Assert.AreEqual(expected.SearchRange, mockWriter.WrittenObjects[2]);
            Assert.AreEqual(expected.EntrySelector, mockWriter.WrittenObjects[3]);
            Assert.AreEqual(expected.RangeShift, mockWriter.WrittenObjects[4]);

            int currentIndex = 5;
            for (int i = 0; i < expected.NumberOfTables; i++)
                currentIndex = VerifyTableRecordWasWritten(mockWriter, currentIndex, expected.Records[i]);
            Assert.AreEqual(currentIndex, mockWriter.WrittenObjects.Count);
        }
        private static int VerifyTableRecordWasWritten(MockFontWriter mockWriter, int startIndex, TableRecord record)
        {
            Assert.AreEqual(record.Tag, mockWriter.WrittenObjects[startIndex]);
            Assert.AreEqual(record.Checksum, mockWriter.WrittenObjects[startIndex + 1]);
            return startIndex + 2;
        }

        [TestInitialize]
        public void BuildObj()
        {
            _obj = new OffsetPersistor(MockPersistorCollection);
        }

        [DataTestMethod]
        [DynamicData(nameof(SampleData), DynamicDataSourceType.Property)]
        public void ReadAddsTable(OffsetTable sample)
        {
            PrimeTableToRead(MockReader, sample);
            TestObject.Read(MockReader);
            Assert.AreEqual(1, MockTableCollection.TablesAdded.Count);
        }

        [DataTestMethod]
        [DynamicData(nameof(SampleData), DynamicDataSourceType.Property)]
        public void AddedTableIsExpected(OffsetTable sample)
        {
            PrimeTableToRead(MockReader, sample);
            TestObject.Read(MockReader);
            VerifyTablesAreEqual(sample, MockTableCollection.TablesAdded[0] as OffsetTable);
        }

        [DataTestMethod]
        [DynamicData(nameof(SampleData), DynamicDataSourceType.Property)]
        public void ReadAddsOffsetsToRegistry(OffsetTable sample)
        {
            PrimeTableToRead(MockReader, sample);
            TestObject.Read(MockReader);
            Assert.AreEqual(sample.Records.Count, MockOffsetRegistry.RegisteredParsers.Count);

            for (int i = 0; i < sample.Records.Count; i++)
                Assert.AreEqual(sample.Records[i].Offset, MockOffsetRegistry.RegisteredParsers[i].offset);
        }

        [DataTestMethod]
        [DynamicData(nameof(SampleData), DynamicDataSourceType.Property)]
        public void ReadRequestsRecordByTag(OffsetTable sample)
        {
            PrimeTableToRead(MockReader, sample);
            TestObject.Read(MockReader);
            Assert.AreEqual(sample.Records.Count, MockPersistorCollection.TagsRetrieved.Count);

            for (int i = 0; i < sample.Records.Count; i++)
                Assert.AreEqual(sample.Records[i].Tag, MockPersistorCollection.TagsRetrieved[i]);
        }

        [TestMethod]
        [ExpectedException(typeof(MisorderedTagsException))]
        public void ReadThrowsIfTagsAreMisordered()
        {
            PrimeTableToRead(MockReader, Mock.MockTable.Misordered);
            TestObject.Read(MockReader);
        }

        [DataTestMethod]
        [DynamicData(nameof(SampleData), DynamicDataSourceType.Property)]
        public void WritesCorrectData(OffsetTable sample)
        {
            TestObject.Write(MockWriter, sample);
            VerifyTableWasWritten(MockWriter, sample);
        }

        [DataTestMethod]
        [DynamicData(nameof(SampleData), DynamicDataSourceType.Property)]
        public void WriteDefersOffsetAndLength(OffsetTable sample)
        {
            TestObject.Write(MockWriter, sample);
            Assert.AreEqual(sample.Records.Count * 2, MockWriter.DeferredUInt32s.Count);
        }

        private uint ExecuteFirstDeferredWrite(int record) => MockWriter.DeferredUInt32s[(record * 2)](MockTableCollection, MockOffsetRegistry);
        private uint ExecuteSecondDeferredWrite(int record) => MockWriter.DeferredUInt32s[(record * 2) + 1](MockTableCollection, MockOffsetRegistry);

        [DataTestMethod]
        [DynamicData(nameof(SampleData), DynamicDataSourceType.Property)]
        public void FirstDeferredWriteRetrievesTableByTag(OffsetTable sample)
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
        public void FirstDeferredWriteRetrievesOffsetWithPrimaryTableKey(OffsetTable sample)
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
        public void FirstDeferredWriteReturnsOffsetFromKey(OffsetTable sample)
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
        public void SecondDeferredWriteRetrievesTableByTag(OffsetTable sample)
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
        public void SecondDeferredWriteRetrievesLengthWithPrimaryTableKey(OffsetTable sample)
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
        public void SecondDeferredWriteReturnsLengthFromKey(OffsetTable sample)
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
