using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Curds.Domain;
using Curds.Domain.Parsing.CSV;
using System.IO;

namespace Curds.Infrastructure.Parsing.CSV.Tests
{
    [TestClass]
    public class ReadingBuffer : Test
    {
        private const string Example = "The quick brown fox jumps over the lazy dog";

        private CSV.ReadingBuffer BuildBuffer(int? bufferLength)
        {
            if (bufferLength.HasValue)
                return new CSV.ReadingBuffer(bufferLength.Value, ExampleStream, TestEncoding);
            else
                return new CSV.ReadingBuffer(ExampleStream, TestEncoding);
        }
        private Stream ExampleStream => ParsingCase.PrepareTextStream(Example, TestEncoding);
        private Encoding TestEncoding = Encoding.Default;

        private static readonly List<int> ExpectedBufferLengthExceptions = new List<int>()
        {
            { -1 },
            { 0 },
            { 1 },
            { 2 },
        };

        [TestMethod]
        public void BufferLengthExceptions()
        {
            foreach (int badLength in ExpectedBufferLengthExceptions)
                TestForException<Exception>(() => BuildBuffer(badLength));
        }

        private static readonly List<int?> BufferLengthsToTest = new List<int?>()
        {
            { null },
            { 3 },
            { 5 },
            { 7 },
            { 9 },
            { 13 },
            { 15 },
            { 20 },
            { 25 },
        };

        private static readonly List<int> ReadBysToTest = new List<int>()
        {
            { 1 },
            { 4 },
            { 7 },
            { 10 },
            { 13 },
            { 16 },
            { 19 },
        };

        [TestMethod]
        public void ReadByN()
        {
            foreach (int? bufferLength in BufferLengthsToTest)
                foreach (int readBy in ReadBysToTest)
                    ReadExampleByN(BuildBuffer(bufferLength), readBy);

        }
        private void ReadExampleByN(CSV.ReadingBuffer buffer, int readBy)
        {
            int currentIndex = 0;
            while (currentIndex < Example.Length)
            {
                AdvanceByN(buffer, readBy);
                currentIndex += readBy;
                VerifyCharAtIndex(currentIndex, buffer.First);
            }
        }
        private void AdvanceByN(CSV.ReadingBuffer buffer, int numberToReadBy)
        {
            for (int i = 0; i < numberToReadBy; i++)
                buffer.AdvanceReadBuffer();
        }
        private void VerifyCharAtIndex(int index, char? actual)
        {
            char? expected = ExampleIndex(index);

            if (expected == null)
                Assert.IsNull(actual);
            else
                Assert.AreEqual(expected.Value, actual.Value);
        }
        private char? ExampleIndex(int index) => index >= Example.Length ? null : (char?)Example[index];
    }
}
