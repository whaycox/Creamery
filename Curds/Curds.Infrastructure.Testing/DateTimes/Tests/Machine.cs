using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Curds.Application.DateTimes;

namespace Curds.Infrastructure.DateTimes.Tests
{
    [TestClass]
    public class Machine
    {
        private const int AcceptableDeltaInMs = 1;

        private IDateTime Time = new DateTimes.Machine();

        [TestMethod]
        public void MatchesNow()
        {
            DateTimeOffset service = Time.Fetch;
            DateTimeOffset machine = DateTimeOffset.Now;

            TimeSpan delta = machine - service;

            Assert.IsTrue(delta.TotalMilliseconds < AcceptableDeltaInMs);

        }
    }
}
