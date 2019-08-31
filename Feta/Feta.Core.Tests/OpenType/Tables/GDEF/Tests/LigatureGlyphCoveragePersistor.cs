using System;
using System.Collections.Generic;
using System.Text;
using Feta.OpenType.Domain;
using Feta.OpenType.Mock;
using Feta.OpenType.Tables.Coverage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Feta.OpenType.Tables.GDEF.Tests
{
    [TestClass]
    public class LigatureGlyphCoveragePersistor : Coverage.Template.Persistor<Table>
    {
        protected override Persistor<Table> TestObject { get; } = new GDEF.LigatureGlyphCoveragePersistor();

        protected override void PrimeTableToRead(IFontReader mockReader, Coverage.Table table)
        {
            throw new NotImplementedException();
        }

        protected override void VerifyTablesAreEqual(Coverage.Table expected, Coverage.Table actual)
        {
            throw new NotImplementedException();
        }

        protected override void VerifyTableWasWritten(IFontWriter mockWriter, Coverage.Table table)
        {
            throw new NotImplementedException();
        }
    }
}
