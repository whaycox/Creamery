using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Feta.OpenType.Tables.Coverage.Tests
{
    using OpenType.Mock;
    using Template;
    using Mock;
    using Implementation;
    using Domain;

    [TestClass]
    public class PersistorTest : PersistorTemplate<MockPrimaryTable>
    {
        protected override CoveragePersistor<MockPrimaryTable> TestObject { get; } = new MockPersistor();

        protected override void PrimeTableToRead(MockFontReader mockReader, CoverageTable table)
        {
            throw new NotImplementedException();
        }

        protected override void VerifyTablesAreEqual(CoverageTable expected, CoverageTable actual)
        {
            throw new NotImplementedException();
        }

        protected override void VerifyTableWasWritten(MockFontWriter mockWriter, CoverageTable table)
        {
            throw new NotImplementedException();
        }
    }
}
