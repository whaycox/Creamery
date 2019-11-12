using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Gouda.Scheduling.Tests
{
    using Abstraction;
    using Implementation;

    [TestClass]
    public class ScheduleFactoryTest
    {
        private ScheduleFactory TestObject = new ScheduleFactory();

        [TestMethod]
        public void BuildsSchedule()
        {
            Assert.IsInstanceOfType(TestObject.BuildSchedule(), typeof(Schedule));
        }
    }
}
