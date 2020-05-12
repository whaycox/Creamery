using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;

namespace Curds.Persistence.Query.Values.Tests
{
    using Domain;

    [TestClass]
    public class DateTimeValueTest
    {
        private DateTimeValue TestObject = new DateTimeValue();

        [TestMethod]
        public void ContentIsDateTime()
        {
            DateTime testTime = DateTime.Now;
            TestObject.DateTime = testTime;

            Assert.AreEqual(testTime, TestObject.Content);
        }

        [TestMethod]
        public void DataTypeIsExpected()
        {
            Assert.AreEqual(SqlDbType.DateTime, TestObject.DataType);
        }
    }
}
