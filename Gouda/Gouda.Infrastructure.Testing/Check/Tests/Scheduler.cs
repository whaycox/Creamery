using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Curds.Domain.DateTimes;
using Gouda.Domain.Persistence;
using Curds.Infrastructure.Cron;
using Gouda.Domain.Communication;
using System.Threading;
using Curds.Domain;
using Gouda.Domain.Check;
using Gouda.Application.Check;

namespace Gouda.Infrastructure.Check.Tests
{
    [TestClass]
    public class Scheduler : ISchedulerTemplate<Check.Scheduler>
    {
        private Check.Scheduler _obj = null;
        protected override Check.Scheduler TestObject => _obj;

        [TestInitialize]
        public void BuildObj()
        {
            _obj = new Check.Scheduler(Time, Persistence, Sender, 5);
        }

        [TestMethod]
        public void InvalidSleepErrors()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => NullScheduler(0));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => NullScheduler(-1));
        }
        private void NullScheduler(int sleepTime) => new Check.Scheduler(null, null, null, sleepTime);

    }
}
