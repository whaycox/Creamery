using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Curds.Cron.Fields.Tests
{
    using Cron.Abstraction;
    using Implementation;
    using Template;

    [TestClass]
    public class DayOfMonthFieldTest : BaseFieldTemplate
    {
        private DayOfMonthField TestObject = null;
        internal override BaseField BaseFieldTestObject => TestObject;

        protected override void SetTestObject(IEnumerable<ICronRange> ranges) => TestObject = new DayOfMonthField(ranges);
    }
}
