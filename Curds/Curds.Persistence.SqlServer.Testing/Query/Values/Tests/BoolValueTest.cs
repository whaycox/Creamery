using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;

namespace Curds.Persistence.Query.Values.Tests
{
    using Domain;

    [TestClass]
    public class BoolValueTest
    {
        private NullableBoolValue TestObject = new NullableBoolValue();

        [DataTestMethod]
        [DataRow(null)]
        [DataRow(false)]
        [DataRow(true)]
        public void ContentIsBool(bool? testBool)
        {
            TestObject.Bool = testBool;

            Assert.AreEqual(testBool, TestObject.Content);
        }

        [TestMethod]
        public void DataTypeIsExpected()
        {
            Assert.AreEqual(SqlDbType.Bit, TestObject.DataType);
        }
    }
}
