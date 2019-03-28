using Curds.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Queso.Infrastructure.Reading.Tests
{
    [TestClass]
    public class LittleEndianVariableWidth : TestTemplate
    {
        private Reading.LittleEndianVariableWidth TestReader = null;

        [TestMethod]
        public void SupplyingEmptyArrayThrows()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new Reading.LittleEndianVariableWidth(null));
            Assert.ThrowsException<ArgumentNullException>(() => new Reading.LittleEndianVariableWidth(new byte[0]));
        }

        [TestMethod]
        public void InvalidWidthReadThrows()
        {
            TestReader = new Reading.LittleEndianVariableWidth(new byte[1]);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => TestReader.Read(0));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => TestReader.Read(-1));
        }

        [TestMethod]
        public void RequestingMoreBitsThrows()
        {
            TestReader = new Reading.LittleEndianVariableWidth(new byte[1]);
            Assert.ThrowsException<InvalidOperationException>(() => TestReader.Read(9));
        }

        [TestMethod]
        public void RequestingMoreBitsMidByteThrows()
        {
            TestReader = new Reading.LittleEndianVariableWidth(new byte[1]);
            TestReader.Read(7);
            Assert.ThrowsException<InvalidOperationException>(() => TestReader.Read(2));
        }

        [TestMethod]
        public void RetrievesEncodedValues()
        {
            //Little-endian right-to-left
            //56    111000
            //13    1101
            //31    011111
            byte[] encoded = new byte[] { 0x78, 0x7F }; // 56(6bits)|13(4bits)|31(6bits)
            TestReader = new Reading.LittleEndianVariableWidth(encoded);
            Assert.AreEqual(56, TestReader.Read(6));
            Assert.AreEqual(13, TestReader.Read(4));
            Assert.AreEqual(31, TestReader.Read(6));
        }

        [TestMethod]
        public void NegativeOffsetThrows()
        {
            byte[] test = new byte[1];
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Reading.LittleEndianVariableWidth(test, -1));
        }

        [TestMethod]
        public void OffsetBeyondEndThrows()
        {
            byte[] test = new byte[1];
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Reading.LittleEndianVariableWidth(test, 1));
        }

        [TestMethod]
        public void CanStartAtOffset()
        {
            byte[] paddedEncoded = new byte[] { 0x00, 0x00, 0x78, 0x7F };
            TestReader = new Reading.LittleEndianVariableWidth(paddedEncoded, 2);
            Assert.AreEqual(56, TestReader.Read(6));
            Assert.AreEqual(13, TestReader.Read(4));
            Assert.AreEqual(31, TestReader.Read(6));
        }

        [TestMethod]
        public void StartsAtCurrentOffset()
        {
            byte[] test = new byte[1];
            TestReader = new Reading.LittleEndianVariableWidth(test);
            Assert.AreEqual(0, TestReader.UnconsumedIndex);

            test = new byte[5];
            TestReader = new Reading.LittleEndianVariableWidth(test, 3);
            Assert.AreEqual(3, TestReader.UnconsumedIndex);
        }

        [TestMethod]
        public void ConsumingAnyBitPutsIndexAtNextByte()
        {
            byte[] test = new byte[2];
            TestReader = new Reading.LittleEndianVariableWidth(test);
            TestReader.Read(1);
            Assert.AreEqual(1, TestReader.UnconsumedIndex);
        }

        [TestMethod]
        public void UsesNegativeIndexForEnd()
        {
            byte[] test = new byte[1];
            TestReader = new Reading.LittleEndianVariableWidth(test);
            TestReader.Read(1);
            Assert.AreEqual(-1, TestReader.UnconsumedIndex);
        }
    }
}
