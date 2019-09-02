using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Feta.OpenType.Tables.ClassDefinition.Tests
{
    using OpenType.Mock;
    using Mock;
    using Template;
    using Implementation;
    using Domain;

    [TestClass]
    public class PersistorTest : PersistorTemplate<MockPrimaryTable>
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
            PrimeReader(sample);
            TestObject.Read(MockReader);
            Assert.IsNotNull(MockPrimaryTable.Table);
            VerifyTablesAreEqual(sample, MockPrimaryTable.Table as ClassDefinitionTable);
        }

        [DataTestMethod]
        [DynamicData(nameof(DynamicData), DynamicDataSourceType.Property)]
        public void WritesCorrectTable(ClassDefinitionTable sample)
        {
            TestObject.Write(MockWriter, sample);
            VerifyTableWasWritten(sample);
        }

        protected override void PrimeTableToRead(MockFontReader mockReader, ClassDefinitionTable table)
        {
            throw new NotImplementedException();
        }

        protected override void VerifyTableWasWritten(MockFontWriter mockWriter, ClassDefinitionTable table)
        {
            throw new NotImplementedException();
        }
    }
}
