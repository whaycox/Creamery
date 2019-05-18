using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Cron.Range.Template
{
    public abstract class Unbounded<T> : Basic<T> where T : Implementation.Unbounded
    {
        [TestMethod]
        public void IsValidForAnyAbsolutes()
        {
            Assert.IsTrue(TestObject.IsValid(int.MinValue, int.MaxValue));
            Assert.IsTrue(TestObject.IsValid(int.MaxValue, int.MinValue));
        }
    }
}
