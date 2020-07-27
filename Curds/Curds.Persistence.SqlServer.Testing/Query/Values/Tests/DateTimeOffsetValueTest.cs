using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;

namespace Curds.Persistence.Query.Values.Tests
{
    using Domain;

    [TestClass]
    public class DateTimeOffsetValueTest
    {
        private DateTimeOffsetValue TestObject = new DateTimeOffsetValue();

        [TestMethod]
        public void ContentIsDateTimeOffset()
        {
            DateTimeOffset testTime = DateTimeOffset.Now;
            TestObject.DateTimeOffset = testTime;

            Assert.AreEqual(testTime, TestObject.Content);
        }

        [TestMethod]
        public void DataTypeIsExpected()
        {
            Assert.AreEqual(SqlDbType.DateTimeOffset, TestObject.DataType);
        }
    }
}
