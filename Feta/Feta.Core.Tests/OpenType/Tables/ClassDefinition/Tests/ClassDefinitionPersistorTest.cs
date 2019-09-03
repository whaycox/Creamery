using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Feta.OpenType.Tables.ClassDefinition.Tests
{
    using Domain;
    using Exceptions;
    using Implementation;
    using Mock;
    using OpenType.Mock;
    using Template;

    [TestClass]
    public class ClassDefinitionPersistorTest : ClassDefinitionPersistorTemplate<MockPrimaryTable>
    {
        private static IEnumerable<object[]> DynamicData => MockClassDefinitionTable.DynamicData;

        protected override ClassDefinitionPersistor<MockPrimaryTable> TestObject { get; } = new MockClassDefinitionPersistor();

        [TestInitialize]
        public void SetPrimaryTable()
        {
            MockTableCollection.TableToRetrieve = MockPrimaryTable;
        }

        [DataTestMethod]
        [DynamicData(nameof(DynamicData), DynamicDataSourceType.Property)]
        public void ReadAttachesTable(ClassDefinitionTable sample)
        {
            PrimeTableToRead(MockReader, sample);
            TestObject.Read(MockReader);
            Assert.IsNotNull(MockPrimaryTable.Table);
            VerifyTablesAreEqual(sample, MockPrimaryTable.Table as ClassDefinitionTable);
        }

        [TestMethod]
        [ExpectedException(typeof(RangeFormatException))]
        public void OverlappingClassRangeRecordsThrowOnRead()
        {
            ClassDefinitionTable sample = MockClassDefinitionTable.Overlapping;
            PrimeTableToRead(MockReader, sample);
            TestObject.Read(MockReader);
        }

        [TestMethod]
        [ExpectedException(typeof(RangeFormatException))]
        public void MisorderedClassRangeRecordsThrowOnRead()
        {
            ClassDefinitionTable sample = MockClassDefinitionTable.Misordered;
            PrimeTableToRead(MockReader, sample);
            TestObject.Read(MockReader);
        }

        [DataTestMethod]
        [DynamicData(nameof(DynamicData), DynamicDataSourceType.Property)]
        public void WritesTableCorrectly(ClassDefinitionTable sample)
        {
            TestObject.Write(MockWriter, sample);
            VerifyTableWasWritten(MockWriter, sample);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void WrongGlyphCountThrowsOnWrite()
        {
            ClassDefinitionTable sample = MockClassDefinitionTable.One;
            sample.GlyphCount = (ushort)(sample.GlyphCount * 2);
            TestObject.Write(MockWriter, sample);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void WrongClassRangeCountThrowsOnWrite()
        {
            ClassDefinitionTable sample = MockClassDefinitionTable.Four;
            sample.ClassRangeCount = (ushort)(sample.ClassRangeCount * 2);
            TestObject.Write(MockWriter, sample);
        }

        [TestMethod]
        [ExpectedException(typeof(RangeFormatException))]
        public void OverlappingClassRangeRecordsThrowOnWrite()
        {
            ClassDefinitionTable sample = MockClassDefinitionTable.Overlapping;
            TestObject.Write(MockWriter, sample);
        }

        [TestMethod]
        [ExpectedException(typeof(RangeFormatException))]
        public void MisorderedClassRangeRecordsThrowOnWrite()
        {
            ClassDefinitionTable sample = MockClassDefinitionTable.Misordered;
            TestObject.Write(MockWriter, sample);
        }
    }
}
