using Curds;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text;

namespace Feta.OpenType.Tests
{
    using Abstraction;

    [TestClass]
    public class FontWriterTest : Test<Implementation.FontWriter>
    {
        private const ushort TestUInt16 = 37489;
        private static byte[] ExpectedUInt16 => new byte[] { 0x92, 0x71 };
        private const uint TestUInt32 = 2183211076;
        private static byte[] ExpectedUInt32 => new byte[] { 0x82, 0x21, 0x28, 0x44 };
        private const string Test = nameof(Test);

        private Mock.MockTableCollection MockTableCollection = new Mock.MockTableCollection();
        private Mock.MockOffsetRegistry MockOffsetRegistry = new Mock.MockOffsetRegistry();

        private Implementation.FontWriter _obj = null;
        protected override Implementation.FontWriter TestObject => _obj;

        private void VerifyResults(byte[] expected)
        {
            byte[] actual = TestObject.GetBytes();
            Assert.AreEqual(expected.Length, actual.Length);
            for (int i = 0; i < expected.Length; i++)
                Assert.AreEqual(expected[i], actual[i]);
        }

        [TestInitialize]
        public void BuildWriter()
        {
            _obj = new Implementation.FontWriter(MockTableCollection, MockOffsetRegistry);
        }

        [TestMethod]
        public void WritesUInt32()
        {
            TestObject.WriteUInt32(TestUInt32);
            VerifyResults(ExpectedUInt32);
        }

        [TestMethod]
        public void WritesUInt16()
        {
            TestObject.WriteUInt16(TestUInt16);
            VerifyResults(ExpectedUInt16);
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("    ")]
        [DataRow("a")]
        [DataRow("ab")]
        [DataRow("abc")]
        [DataRow("abcde")]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidStringThrows(string testTag)
        {
            TestObject.WriteTag(testTag);
        }

        [TestMethod]
        public void WritesTag()
        {
            TestObject.WriteTag(Test);
            byte[] expected = Encoding.ASCII.GetBytes(Test);
            VerifyResults(expected);
        }

        [TestMethod]
        public void CanDeferWriteUInt16()
        {
            TestObject.DeferWriteUInt16(TestUInt16Deferrer);
            VerifyResults(ExpectedUInt16);
        }
        private ushort TestUInt16Deferrer(ITableCollection tableCollection, IOffsetRegistry offsetRegistry) => TestUInt16;

        [TestMethod]
        public void CanDeferWriteUInt32()
        {
            TestObject.DeferWriteUInt32(TestUInt32Deferrer);
            VerifyResults(ExpectedUInt32);
        }
        private uint TestUInt32Deferrer(ITableCollection tableCollection, IOffsetRegistry offsetRegistry) => TestUInt32;
    }
}
