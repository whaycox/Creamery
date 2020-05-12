using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;

namespace Curds.Persistence.Query.Values.Tests
{
    using Domain;

    [TestClass]
    public class DecimalValueTest
    {
        private DecimalValue TestObject = new DecimalValue();

        [TestMethod]
        public void ContentIsDecimal()
        {
            decimal testDecimal = 3e8m;
            TestObject.Decimal = testDecimal;

            Assert.AreEqual(testDecimal, TestObject.Content);
        }

        [TestMethod]
        public void DataTypeIsExpected()
        {
            Assert.AreEqual(SqlDbType.Decimal, TestObject.DataType);
        }
    }
}
