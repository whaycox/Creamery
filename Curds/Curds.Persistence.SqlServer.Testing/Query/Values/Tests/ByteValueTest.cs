using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Data;

namespace Curds.Persistence.Query.Values.Tests
{
    using Domain;

    [TestClass]
    public class ByteValueTest
    {
        private NullableByteValue TestObject = new NullableByteValue();

        [DataTestMethod]
        [DynamicData(nameof(ByteData))]
        public void ContentIsByte(byte? testByte)
        {
            TestObject.Byte = testByte;

            Assert.AreEqual(testByte, TestObject.Content);
        }
        private static IEnumerable<object[]> ByteData => new List<object[]>
        {
            new object[] { null },
            new object[] { (byte)0x00 },
            new object[] { (byte)0x16 },
            new object[] { (byte)0xFF },
        };

        [TestMethod]
        public void DataTypeIsExpected()
        {
            Assert.AreEqual(SqlDbType.TinyInt, TestObject.DataType);
        }
    }
}
