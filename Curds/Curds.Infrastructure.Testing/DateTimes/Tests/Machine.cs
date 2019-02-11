using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Curds.Application.DateTimes;

namespace Curds.Infrastructure.DateTimes.Tests
{
    [TestClass]
    public class Machine : DateTimeTemplate<DateTimes.Machine>
    {
        private const int AcceptableDeltaInMs = 1;

        private DateTimes.Machine _obj = new DateTimes.Machine();
        protected override DateTimes.Machine TestObject => _obj;

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
