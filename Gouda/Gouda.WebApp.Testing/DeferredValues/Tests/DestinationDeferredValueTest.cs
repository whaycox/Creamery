using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Gouda.WebApp.DeferredValues.Tests
{
    using Implementation;
    using Application.DeferredValues.Domain;

    [TestClass]
    public class DestinationDeferredValueTest
    {
        private DestinationDeferredValue TestObject = new DestinationDeferredValue();

        [TestMethod]
        public void HasValueForAllKeys()
        {
            foreach (DestinationDeferredKey key in Enum.GetValues(typeof(DestinationDeferredKey)))
                if (key != DestinationDeferredKey.None)
                    Assert.IsNotNull(TestObject[key]);
        }

    }
}
