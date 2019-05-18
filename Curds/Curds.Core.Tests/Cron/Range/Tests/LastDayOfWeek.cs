using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Curds.Cron.Range.Tests
{
    [TestClass]
    public class LastDayOfWeek : Template.DayOfWeek<Implementation.LastDayOfWeek>
    {
        protected override DayOfWeek TestDayOfWeek => DayOfWeek.Monday;

        private Implementation.LastDayOfWeek _obj = null;
        protected override Implementation.LastDayOfWeek TestObject => _obj;

        [TestInitialize]
        public void Init()
        {
            _obj = new Implementation.LastDayOfWeek(TestArg);
        }

        [TestMethod]
        public void IsTrueOnLastOccurrenceOnly()
        {
            Assert.IsFalse(TestObject.Test(MockToken, new DateTime(2019, 05, 06)));
            Assert.IsFalse(TestObject.Test(MockToken, new DateTime(2019, 05, 13)));
            Assert.IsFalse(TestObject.Test(MockToken, new DateTime(2019, 05, 20)));
            Assert.IsTrue(TestObject.Test(MockToken, new DateTime(2019, 05, 27)));
        }

        [TestMethod]
        public void IsTrueWhenLastIsLastDay()
        {
            Assert.IsFalse(TestObject.Test(MockToken, new DateTime(2018, 12, 03)));
            Assert.IsFalse(TestObject.Test(MockToken, new DateTime(2018, 12, 10)));
            Assert.IsFalse(TestObject.Test(MockToken, new DateTime(2018, 12, 17)));
            Assert.IsFalse(TestObject.Test(MockToken, new DateTime(2018, 12, 24)));
            Assert.IsTrue(TestObject.Test(MockToken, new DateTime(2018, 12, 31)));
        }

        [TestMethod]
        public void IsTrueWhenLastIsSixAwayFromLastDay()
        {
            Assert.IsFalse(TestObject.Test(MockToken, new DateTime(2019, 03, 04)));
            Assert.IsFalse(TestObject.Test(MockToken, new DateTime(2019, 03, 11)));
            Assert.IsFalse(TestObject.Test(MockToken, new DateTime(2019, 03, 18)));
            Assert.IsTrue(TestObject.Test(MockToken, new DateTime(2019, 03, 25)));
            Assert.IsFalse(TestObject.Test(MockToken, new DateTime(2019, 04, 01)));
        }
    }
}
