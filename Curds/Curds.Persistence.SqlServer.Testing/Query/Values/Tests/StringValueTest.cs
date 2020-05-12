using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;

namespace Curds.Persistence.Query.Values.Tests
{
    using Domain;

    [TestClass]
    public class StringValueTest
    {
        private StringValue TestObject = new StringValue();

        [TestMethod]
        public void ContentIsString()
        {
            TestObject.String = nameof(ContentIsString);

            Assert.AreEqual(nameof(ContentIsString), TestObject.Content);
        }

        [TestMethod]
        public void DataTypeIsExpected()
        {
            Assert.AreEqual(SqlDbType.NVarChar, TestObject.DataType);
        }
    }
}
