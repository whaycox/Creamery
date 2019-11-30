using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Gouda.Application.DeferredValues.Tests
{
    using Implementation;
    using Domain;

    [TestClass]
    public class LabelDeferredValueTest
    {
        private LabelDeferredValue TestObject = new LabelDeferredValue();

        [TestMethod]
        public void HasValueForAllKeys()
        {
            foreach (LabelDeferredKey key in Enum.GetValues(typeof(LabelDeferredKey)))
                if (key != LabelDeferredKey.None)
                    Assert.IsFalse(string.IsNullOrWhiteSpace(TestObject[key]));
        }
    }
}
