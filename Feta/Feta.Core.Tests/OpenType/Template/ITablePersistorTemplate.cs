using Curds;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Collections.Generic;

namespace Feta.OpenType.Template
{
    using Abstraction;
    using Domain;
    using Mock;

    [TestClass]
    public abstract class ITablePersistorTemplate<T, U> : TableTemplate<T>
        where T : ITablePersistor
        where U : BaseTable
    {
        protected abstract void PrimeTableToRead(MockFontReader mockReader, U table);
        protected abstract void VerifyTablesAreEqual(U expected, U actual);
        protected abstract void VerifyTableWasWritten(MockFontWriter mockWriter, U expected);
    }
}
