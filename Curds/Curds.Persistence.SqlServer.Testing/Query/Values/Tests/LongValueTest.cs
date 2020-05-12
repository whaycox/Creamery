using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;

namespace Curds.Persistence.Query.Values.Tests
{
    using Domain;

    [TestClass]
    public class LongValueTest
    {
        private LongValue TestObject = new LongValue();

        [TestMethod]
        public void ContentIsLong()
        {
            long testLong = 123456;
            TestObject.Long = testLong;

            Assert.AreEqual(testLong, TestObject.Content);
        }

        [TestMethod]
        public void DataTypeIsExpected()
        {
            Assert.AreEqual(SqlDbType.BigInt, TestObject.DataType);
        }
    }
}
