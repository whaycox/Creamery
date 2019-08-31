using Curds;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Collections.Generic;

namespace Feta.OpenType.Tables.Template
{
    using Abstraction;
    using OpenType.Domain;
    using OpenType.Template;
    using OpenType.Mock;

    [TestClass]
    public abstract class ITablePersistor<T, U> : Table<T>
        where T : ITablePersistor<U>
        where U : BaseTable
    {
        protected abstract void PrimeTableToRead(IFontReader mockReader, U table);
        protected abstract void VerifyTablesAreEqual(U expected, U actual);
        protected abstract void VerifyTableWasWritten(IFontWriter mockWriter, U table);
    }
}
