using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Feta.OpenType.Template
{
    using Domain;
    using Implementation;

    public abstract class PrimaryTablePersistorTemplate<T, U> : ITablePersistorTemplate<T, U>
        where T : PrimaryTablePersistor<U>
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
