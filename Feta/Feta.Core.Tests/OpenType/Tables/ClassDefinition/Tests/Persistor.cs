using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Feta.OpenType.Tables.ClassDefinition.Tests
{
    using OpenType.Mock;

    [TestClass]
    public class Persistor : Template.Persistor<PrimaryTable>
    {
        private static IEnumerable<object[]> DynamicData => Mock.Table.DynamicData;

        protected override Persistor<PrimaryTable> TestObject { get; } = new Mock.Persistor();

        [TestInitialize]
        public void SetPrimaryTable()
        {
            MockTableCollection.TableToRetrieve = MockPrimaryTable;
        }

        [DataTestMethod]
        [DynamicData(nameof(DynamicData), DynamicDataSourceType.Property)]
        public void ReadAttachesTable(ClassDefinition.Table sample)
        {
            PrimeReader(sample);
            TestObject.Read(MockReader);
            Assert.IsNotNull(MockPrimaryTable.Table);
            VerifyTablesAreEqual(sample, MockPrimaryTable.Table as ClassDefinition.Table);
        }

        [DataTestMethod]
        [DynamicData(nameof(DynamicData), DynamicDataSourceType.Property)]
        public void WritesCorrectTable(ClassDefinition.Table sample)
        {
            TestObject.Write(MockWriter, sample);
            VerifyTableWasWritten(sample);
        }

        protected override void PrimeTableToRead(IFontReader mockReader, ClassDefinition.Table table)
        {
            throw new NotImplementedException();
        }

        protected override void VerifyTableWasWritten(IFontWriter mockWriter, ClassDefinition.Table table)
        {
            throw new NotImplementedException();
        }
    }
}
