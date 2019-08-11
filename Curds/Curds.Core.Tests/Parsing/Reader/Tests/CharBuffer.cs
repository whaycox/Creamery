using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text;

namespace Curds.Parsing.Reader.Tests
{
    [TestClass]
    public class CharBuffer : Test<Implementation.CharBuffer>
    {
        private const string TestingString = "The quick\rbrown fox jumps\r\nover the lazy\ndog";
        private static char? ExpectedChar(int index) => index >= TestingString.Length ? null as char? : TestingString[index];

        private const int TestBufferSize = 5;
        private const int TestLookaheads = 3;
        private static readonly Encoding TestEncoding = Encoding.UTF8;

        private Stream TestingStream => new MemoryStream(Encoding.UTF8.GetBytes(TestingString));
        private Domain.CharReaderOptions TestingOptions => new Domain.CharReaderOptions() { BufferSize = TestBufferSize, TextEncoding = TestEncoding, Lookaheads = TestLookaheads };

        private Implementation.CharBuffer _obj = null;
        protected override Implementation.CharBuffer TestObject => _obj;

        [TestInitialize]
        public void BuildObj()
        {
            _obj = new Implementation.CharBuffer(TestingStream, TestingOptions);
        }

        [TestCleanup]
        public void Dispose()
        {
            TestObject.Dispose();
        }

        private void ReadTestingString(Implementation.CharBuffer buffer)
        {
            StringBuilder result = new StringBuilder();
            while (!buffer.IsConsumed)
                result.Append(buffer.Advance());

            Assert.AreEqual(TestingString, result.ToString());
        }

        private void ReadTestingStringWithLookaheads(Implementation.CharBuffer buffer, int lookaheads)
        {
            for (int i = 0; i < TestingString.Length; i++)
            {
                for (int j = 0; j < lookaheads; j++)
                    Assert.AreEqual(ExpectedChar(i + j), buffer[j]);
                buffer.Advance();
            }
        }

        [TestMethod]
        public void CanReadTestingString() => ReadTestingString(TestObject);

        [TestMethod]
        public void CanLookaheadProperly() => ReadTestingStringWithLookaheads(TestObject, TestLookaheads);

        [TestMethod]
        public void CanReadStringWithNoLookahead()
        {
            var options = TestingOptions;
            options.Lookaheads = 0;
            using (Implementation.CharBuffer testBuffer = new Implementation.CharBuffer(TestingStream, options))
                ReadTestingString(testBuffer);
        }

        [TestMethod]
        public void ThrowsIfMoreThanLookaheadSpecified()
        {
            Assert.ThrowsException<IndexOutOfRangeException>(() => TestObject[TestLookaheads + 1]);
        }

        [TestMethod]
        public void ThrowsIfNegativeOffsetSpecified()
        {
            Assert.ThrowsException<IndexOutOfRangeException>(() => TestObject[-1]);
        }

        [TestMethod]
        public void InvalidBufferSizeThrows()
        {
            var options = TestingOptions;

            options.BufferSize = -1;
            using (Stream stream = TestingStream)
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Implementation.CharBuffer(stream, options));

            options.BufferSize = 0;
            using (Stream stream = TestingStream)
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Implementation.CharBuffer(stream, options));
        }

        [TestMethod]
        public void ThrowsWithBufferSameAsLookahead()
        {
            var options = TestingOptions;

            options.BufferSize = TestLookaheads;
            using (Stream stream = TestingStream)
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Implementation.CharBuffer(stream, options));
        }

        [TestMethod]
        public void CanReadWithManyBuffers()
        {
            var options = TestingOptions;
            for (int i = TestLookaheads + 1; i <= TestingString.Length; i++)
            {
                options.BufferSize = i;
                using (Stream stream = TestingStream)
                    ReadTestingStringWithLookaheads(new Implementation.CharBuffer(stream, options), TestLookaheads);
            }
        }

        [TestMethod]
        public void CanReadWithManyLookaheadsAndBuffers()
        {
            var options = TestingOptions;
            for (int i = 0; i < 10; i++)
            {
                for (int j = i + 1; j < 25; j++)
                {
                    options.Lookaheads = i;
                    options.BufferSize = j;
                    using (Stream stream = TestingStream)
                        ReadTestingStringWithLookaheads(new Implementation.CharBuffer(stream, options), i);
                }
            }
        }

        [TestMethod]
        public void CanReadDifferentEncodings()
        {
            TestUnicodeCharsWithEncoding(Encoding.UTF8);
            TestUnicodeCharsWithEncoding(Encoding.UTF7);
            TestUnicodeCharsWithEncoding(Encoding.UTF32);
            TestUnicodeCharsWithEncoding(Encoding.Unicode);
            TestUnicodeCharsWithEncoding(Encoding.BigEndianUnicode);
        }
        private void TestUnicodeCharsWithEncoding(Encoding encoding)
        {
            var options = TestingOptions;
            options.TextEncoding = encoding;
            using (Stream stream = new MemoryStream(encoding.GetBytes(BuildUnicodeString())))
            {
                var buffer = new Implementation.CharBuffer(stream, options);
                for (int i = RangeStart; i <= RangeEnd; i++)
                    Assert.AreEqual(ExpectedUnicodeChar(i), buffer.Advance());
            }
        }
        private string BuildUnicodeString()
        {
            StringBuilder toReturn = new StringBuilder();
            for (int i = RangeStart; i <= RangeEnd; i++)
                toReturn.Append((char)i);
            return toReturn.ToString();
        }
        private const int RangeStart = 0x0250;
        private const int RangeEnd = 0x02AF;
        private char? ExpectedUnicodeChar(int value)
        {
            if (value < RangeStart || value > RangeEnd)
                return null;
            else
                return (char)value;
        }
    }
}
