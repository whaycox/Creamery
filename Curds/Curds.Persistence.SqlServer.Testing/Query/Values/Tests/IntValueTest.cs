using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;

namespace Curds.Persistence.Query.Values.Tests
{
    using Domain;

    [TestClass]
    public class IntValueTest
    {
        private IntValue TestObject = new IntValue();

        [TestMethod]
        public void ContentIsInt()
        {
            int testInt = int.MinValue;
            TestObject.Int = testInt;

            Assert.AreEqual(testInt, TestObject.Content);
        }

        [TestMethod]
        public void DataTypeIsExpected()
        {
            Assert.AreEqual(SqlDbType.Int, TestObject.DataType);
        }
    }
}
