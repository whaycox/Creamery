using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Feta.OpenType.Tables.Coverage.Tests
{
    using Domain;
    using Exceptions;
    using Implementation;
    using Mock;
    using OpenType.Mock;
    using Template;

    [TestClass]
    public class CoveragePersistorTest : CoveragePersistorTemplate<MockPrimaryTable>
    {
        private static IEnumerable<object[]> DynamicData => MockCoverageTable.DynamicData;

        protected override CoveragePersistor<MockPrimaryTable> TestObject { get; } = new MockCoveragePersistor();

        [TestInitialize]
        public void SetPrimaryTable()
        {
            MockTableCollection.TableToRetrieve = MockPrimaryTable;
        }

        [DataTestMethod]
        [DynamicData(nameof(DynamicData), DynamicDataSourceType.Property)]
        public void ReadAttachesTable(CoverageTable sample)
        {
            PrimeTableToRead(MockReader, sample);
            TestObject.Read(MockReader);
            Assert.IsNotNull(MockPrimaryTable.Table);
            VerifyTablesAreEqual(sample, MockPrimaryTable.Table as CoverageTable);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void MisorderedGlyphArrayThrowsOnRead()
        {
            CoverageTable sample = MockCoverageTable.MisorderedGlyphArray;
            PrimeTableToRead(MockReader, sample);
            TestObject.Read(MockReader);
        }

        [TestMethod]
        [ExpectedException(typeof(RangeFormatException))]
        public void MisorderedRangeRecordsThrowOnRead()
        {
            CoverageTable sample = MockCoverageTable.MisorderedRangeRecord;
            PrimeTableToRead(MockReader, sample);
            TestObject.Read(MockReader);
        }

        [TestMethod]
        [ExpectedException(typeof(RangeFormatException))]
        public void OverlappingRangeRecordsThrowOnRead()
        {
            CoverageTable sample = MockCoverageTable.OverlappingRangeRecord;
            PrimeTableToRead(MockReader, sample);
            TestObject.Read(MockReader);
        }

        [TestMethod]
        [ExpectedException(typeof(RangeFormatException))]
        public void BadStartCoverageIndexThrowOnRead()
        {
            CoverageTable sample = MockCoverageTable.BadStartCoverageIndex;
            PrimeTableToRead(MockReader, sample);
            TestObject.Read(MockReader);
        }

        [TestMethod]
        [DynamicData(nameof(DynamicData), DynamicDataSourceType.Property)]
        public void WritesTableCorrectly(CoverageTable sample)
        {
            TestObject.Write(MockWriter, sample);
            VerifyTableWasWritten(MockWriter, sample);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void WrongGlyphCountThrowsOnWrite()
        {
            CoverageTable sample = MockCoverageTable.One;
            sample.GlyphCount = (ushort)(sample.GlyphCount * 2);
            TestObject.Write(MockWriter, sample);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void MisorderedGlyphArrayThrowsOnWrite()
        {
            CoverageTable sample = MockCoverageTable.MisorderedGlyphArray;
            TestObject.Write(MockWriter, sample);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void WrongRangeCountThrowsOnWrite()
        {
            CoverageTable sample = MockCoverageTable.Three;
            sample.RangeCount = (ushort)(sample.RangeCount * 2);
            TestObject.Write(MockWriter, sample);
        }

        [TestMethod]
        [ExpectedException(typeof(RangeFormatException))]
        public void MisorderedRangeRecordsThrowOnWrite()
        {
            CoverageTable sample = MockCoverageTable.MisorderedRangeRecord;
            TestObject.Write(MockWriter, sample);
        }

        [TestMethod]
        [ExpectedException(typeof(RangeFormatException))]
        public void OverlappingRangeRecordsThrowOnWrite()
        {
            CoverageTable sample = MockCoverageTable.OverlappingRangeRecord;
            TestObject.Write(MockWriter, sample);
        }

        [TestMethod]
        [ExpectedException(typeof(RangeFormatException))]
        public void BadStartCoverageIndexThrowsOnWrite()
        {
            CoverageTable sample = MockCoverageTable.BadStartCoverageIndex;
            TestObject.Write(MockWriter, sample);
        }
    }
}
