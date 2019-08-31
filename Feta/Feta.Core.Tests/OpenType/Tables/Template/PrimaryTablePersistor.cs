using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Feta.OpenType.Tables.Template
{
    using OpenType.Domain;

    public abstract class PrimaryTablePersistor<T, U> : ITablePersistor<T, U>
        where T : Domain.PrimaryTablePersistor<U>
        where U : PrimaryTable
    {
        [TestMethod]
        public void TableOutputIsMultipleOfFour()
        {
            throw new NotImplementedException();
            //foreach (U sample in Samples)
            //{
            //    TestObject.Write(Writer, sample);
            //    Assert.IsTrue(TestStream.Length % 4 == 0);

            //    RefreshStream();
            //}
        }
    }
}
