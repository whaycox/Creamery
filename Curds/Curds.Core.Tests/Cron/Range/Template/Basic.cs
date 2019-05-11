using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Cron.Range.Template
{
    public abstract class Basic<T> : Test where T : Domain.Basic
    {
        private const int LowValue = 2;
        private const int HighValue = 7;

        protected abstract T Build(int min, int max);

        [TestMethod]
        public void InvertedRangeThrows()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Build(HighValue, LowValue));
        }

        [TestMethod]
        public void InvalidOnAbsoluteMin()
        {
            T range = Build(LowValue, HighValue);
            Assert.IsFalse(range.ValidateBounds(LowValue - 1, HighValue));
        }

        [TestMethod]
        public void InvalidOnAbsoluteMax()
        {
            T range = Build(LowValue, HighValue);
            Assert.IsFalse(range.ValidateBounds(LowValue, HighValue + 1));
        }

        [TestMethod]
        public void ValidatesOnAbsolutes()
        {
            T range = Build(LowValue, HighValue);
            Assert.IsTrue(range.ValidateBounds(LowValue, HighValue));
        }

        [TestMethod]
        public void 

    }
}
