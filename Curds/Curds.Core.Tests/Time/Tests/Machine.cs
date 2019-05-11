using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Whey.Template;

namespace Curds.Time.Tests
{
    [TestClass]
    public class Machine : Test<Implementation.Machine>
    {
        private const int AcceptableDeltaInMs = 1;

        private Implementation.Machine _obj = new Implementation.Machine();
        protected override Implementation.Machine TestObject => _obj;

        [TestMethod]
        public void MatchesNow()
        {
            DateTimeOffset service = TestObject.Fetch;
            DateTimeOffset machine = DateTimeOffset.Now;

            TimeSpan delta = machine - service;

            Assert.IsTrue(delta.TotalMilliseconds < AcceptableDeltaInMs);
        }
    }
}
