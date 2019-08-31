using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Feta.OpenType.Tables.Coverage.Tests
{
    using OpenType.Mock;

    [TestClass]
    public class Persistor : Template.Persistor<PrimaryTable>
    {
        protected override Persistor<PrimaryTable> TestObject { get; } = new Mock.Persistor();

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
