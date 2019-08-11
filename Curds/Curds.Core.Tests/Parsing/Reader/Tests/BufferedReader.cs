using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace Curds.Parsing.Reader.Tests
{
    using Domain;

    [TestClass]
    public class BufferedReader : Test<Implementation.BufferedReader>
    {
        private static Random Random { get; } = new Random();

        private byte[] _payload = null;

        private void BuildObj(int payloadSize, int bufferSize, int lookaheads)
        {
            ReaderOptions options = new ReaderOptions
            {
                BufferSize = bufferSize,
                Lookaheads = lookaheads,
            };
            _obj = new Implementation.BufferedReader(PrepareStream(payloadSize), options);
        }
        private Stream PrepareStream(int payloadSize)
        {
            _payload = GenerateTestPayload(payloadSize);
            return new MemoryStream(_payload);
        }
        private byte[] GenerateTestPayload(int size)
        {
            byte[] toReturn = new byte[size];
            Random.NextBytes(toReturn);
            return toReturn;
        }

        private Implementation.BufferedReader _obj = null;
        protected override Implementation.BufferedReader TestObject => _obj;

        [TestCleanup]
        public void DisposeObj()
        {
            TestObject?.Dispose();
        }

        [DataTestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void InvalidBufferSizeThrows(int bufferSize)
        {
            ReaderOptions options = new ReaderOptions
            {
                BufferSize = bufferSize,
            };
            using (Stream stream = PrepareStream(PayloadSizes[0]))
                new Implementation.BufferedReader(stream, options);
        }

        [DataTestMethod]
        [DataRow(10, 11)]
        [DataRow(5, 5)]
        [DataRow(100, 110)]
        [DataRow(1000, 1000)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void InvalidLookaheadThrows(int bufferSize, int lookaheads)
        {
            ReaderOptions options = new ReaderOptions
            {
                BufferSize = bufferSize,
                Lookaheads = lookaheads,
            };
            using (Stream stream = PrepareStream(PayloadSizes[0]))
                new Implementation.BufferedReader(stream, options);
        }

        [TestMethod]
        [DynamicData(nameof(ReaderData), DynamicDataSourceType.Method)]
        public void ParsesStreamCorrectly(int payloadSize, int bufferSize, int lookaheads)
        {
            BuildObj(payloadSize, bufferSize, lookaheads);

            int currentIndex = 0;
            while (!TestObject.IsConsumed)
                TestByte(currentIndex++, payloadSize, lookaheads);
        }
        private void TestByte(int currentIndex, int payloadSize, int lookaheads)
        {
            for (int i = 0; i <= lookaheads; i++)
                TestAtIndex(currentIndex + i, i);
            Assert.AreEqual(ExpectedAt(currentIndex), TestObject.Advance());
        }
        private void TestAtIndex(int index, int offset)
        {
            Assert.AreEqual(ExpectedAt(index), TestObject[offset]);
        }
        private byte? ExpectedAt(int index)
        {
            if (index >= _payload.Length)
                return null;
            return _payload[index];
        }

        private static IEnumerable<object[]> ReaderData()
        {
            for (int i = 0; i < PayloadSizes.Length; i++)
                for (int j = 0; j < BufferSizes.Length; j++)
                    for (int k = 0; k < Lookaheads.Length; k++)
                        yield return new object[] { PayloadSizes[i], BufferSizes[j], Lookaheads[k] };
        }
        private static readonly int[] PayloadSizes = new int[]
        {
            16,
            32,
            64,
            200,
            750,
            1500,
        };
        private static readonly int[] BufferSizes = new int[]
        {
            16,
            32,
            45,
            65,
            100,
            65535,
        };
        private static readonly int[] Lookaheads = new int[]
        {
            3,
            5,
            8,
            15,
        };
    }
}
