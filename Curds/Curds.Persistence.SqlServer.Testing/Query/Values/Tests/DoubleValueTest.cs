using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;

namespace Curds.Persistence.Query.Values.Tests
{
    using Domain;

    [TestClass]
    public class DoubleValueTest
    {
        private DoubleValue TestObject = new DoubleValue();

        [TestMethod]
        public void ContentIsDouble()
        {
            double testDouble = double.MaxValue;
            TestObject.Double = testDouble;

            Assert.AreEqual(testDouble, TestObject.Content);
        }

        [TestMethod]
        public void DataTypeIsExpected()
        {
            Assert.AreEqual(SqlDbType.Float, TestObject.DataType);
        }
    }
}
