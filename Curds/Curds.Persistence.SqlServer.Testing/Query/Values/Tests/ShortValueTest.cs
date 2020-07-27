using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;

namespace Curds.Persistence.Query.Values.Tests
{
    using Domain;

    [TestClass]
    public class ShortValueTest
    {
        private ShortValue TestObject = new ShortValue();

        [TestMethod]
        public void ContentIsShort()
        {
            short testShort = 123;
            TestObject.Short = testShort;

            Assert.AreEqual(testShort, TestObject.Content);
        }

        [TestMethod]
        public void DataTypeIsExpected()
        {
            Assert.AreEqual(SqlDbType.SmallInt, TestObject.DataType);
        }
    }
}
