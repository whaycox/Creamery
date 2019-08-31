using Curds;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Text;

namespace Feta.OpenType.Tests
{
    [TestClass]
    public class FontReader : Test
    {
        private Stream BuildTestStream(byte[] bytes) => new MemoryStream(bytes);

        [TestMethod]
        public void CurrentOffsetIncrements()
        {
            byte[] testBytes = new byte[12];
            using (Implementation.FontReader reader = new Implementation.FontReader(BuildTestStream(testBytes)))
                for (uint i = 0; i < 3; i++)
                {
                    reader.ReadUInt32();
                    Assert.AreEqual((i + 1) * 4, reader.CurrentOffset);
                }
        }

        [TestMethod]
        public void IsConsumedTracksStream()
        {
            byte[] testBytes = new byte[12];
            using (Implementation.FontReader reader = new Implementation.FontReader(BuildTestStream(testBytes)))
            {
                for (int i = 0; i < 3; i++)
                {
                    Assert.IsFalse(reader.IsConsumed);
                    reader.ReadUInt32();
                }
                Assert.IsTrue(reader.IsConsumed);
            }
        }

        [TestMethod]
        public void ReadsUInt()
        {
            using (Implementation.FontReader reader = new Implementation.FontReader(BuildTestStream(TestUInt)))
                Assert.AreEqual(ExpectedUInt, reader.ReadUInt32());
        }
        private byte[] TestUInt => new byte[] { 0x96, 0x29, 0xA6, 0xAA };
        private uint ExpectedUInt = 2519312042;

        [TestMethod]
        public void ReadsUShort()
        {
            using (Implementation.FontReader reader = new Implementation.FontReader(BuildTestStream(TestUShort)))
                Assert.AreEqual(ExpectedUShort, reader.ReadUInt16());
        }
        private byte[] TestUShort => new byte[] { 0x84, 0x85 };
        private const ushort ExpectedUShort = 33925;

        [TestMethod]
        public void ReadsTag()
        {
            byte[] testBytes = Encoding.ASCII.GetBytes(Test);
            using (Implementation.FontReader reader = new Implementation.FontReader(BuildTestStream(testBytes)))
                Assert.AreEqual(Test, reader.ReadTag());
        }
        private const string Test = nameof(Test);

        [TestMethod]
        public void TagIsFourChars()
        {
            byte[] testBytes = Encoding.ASCII.GetBytes(nameof(TagIsFourChars));
            using (Implementation.FontReader reader = new Implementation.FontReader(BuildTestStream(testBytes)))
            {
                Assert.AreEqual("TagI", reader.ReadTag());
                Assert.AreEqual("sFou", reader.ReadTag());
                Assert.AreEqual("rCha", reader.ReadTag());
            }
        }
    }
}
