using Curds;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text;

namespace Feta.OpenType.Tests
{
    using Enumerations;

    [TestClass]
    public class FontWriter : Test<Domain.FontWriter>
    {
        private Stream TestStream = new MemoryStream();

        private Domain.FontWriter _obj = null;
        protected override Domain.FontWriter TestObject => _obj;

        private byte[] Results
        {
            get
            {
                TestStream.Seek(0, SeekOrigin.Begin);
                byte[] toReturn = new byte[TestStream.Length];
                TestStream.Read(toReturn, 0, toReturn.Length);
                return toReturn;
            }
        }

        private void VerifyResults(byte[] expected)
        {
            byte[] actual = Results;
            Assert.AreEqual(expected.Length, actual.Length);
            for (int i = 0; i < expected.Length; i++)
                Assert.AreEqual(expected[i], actual[i]);
        }

        [TestInitialize]
        public void BuildObj()
        {
            _obj = new Domain.FontWriter(TestStream);
        }

        [TestCleanup]
        public void Dispose()
        {
            _obj.Dispose();
            TestStream.Dispose();
        }

        [TestMethod]
        public void WritesSfntVersion()
        {
            TestObject.WriteSfntVersion(SfntVersion.CffDataOneAndTwo);
            VerifyResults(ExpectedCffDataOneAndTwo);
        }
        private byte[] ExpectedCffDataOneAndTwo => new byte[] { 0x4F, 0x54, 0x54, 0x4F };

        [TestMethod]
        public void WritesUInt()
        {
            TestObject.WriteUInt32(TestUInt);
            VerifyResults(ExpectedUInt);
        }
        private uint TestUInt = 2183211076;
        private byte[] ExpectedUInt => new byte[] { 0x82, 0x21, 0x28, 0x44 };

        [TestMethod]
        public void WritesUShort()
        {
            TestObject.WriteUInt16(TestUShort);
            VerifyResults(ExpectedUShort);
        }
        private ushort TestUShort = 37489;
        private byte[] ExpectedUShort => new byte[] { 0x92, 0x71 };

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
        private const string Test = nameof(Test);
    }
}
