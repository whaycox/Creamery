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
        private Check.Scheduler _obj = new Check.Scheduler(5);
        protected override Check.Scheduler TestObject => _obj;

        [TestMethod]
        public void InvalidSleepErrors()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Check.Scheduler(0));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Check.Scheduler(-1));
        }

    }
}
