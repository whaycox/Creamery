using System;
using System.Collections.Generic;
using System.Text;
using Feta.OpenType.Domain;
using Feta.OpenType.Mock;
using Feta.OpenType.Tables.Coverage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Feta.OpenType.Tables.GDEF.Tests
{
    using Coverage.Template;
    using Coverage.Domain;
    using Coverage.Implementation;
    using Domain;
    using Implementation;

    [TestClass]
    public class LigatureGlyphCoveragePersistorTest : PersistorTemplate<GdefTable>
    {
        protected override CoveragePersistor<GdefTable> TestObject { get; } = new LigatureGlyphCoveragePersistor();

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
