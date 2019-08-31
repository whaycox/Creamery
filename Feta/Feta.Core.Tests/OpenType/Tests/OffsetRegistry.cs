using Curds;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Feta.OpenType.Tests
{
    using Abstraction;
    using Exceptions;

    [TestClass]
    public class OffsetRegistry : Test<Domain.OffsetRegistry>
    {
        private const uint TestOffset = 5;
        private static readonly object TestKey = new object();
        private static void TestParserDelegate(IFontReader reader) => Assert.IsNull(reader);

        protected override Domain.OffsetRegistry TestObject { get; } = new Domain.OffsetRegistry();

        [TestMethod]
        public void CanRegisterParser()
        {
            TestObject.RegisterParser(TestOffset, TestParserDelegate);
        }

        [TestMethod]
        public void CanRetrieveRegisteredParser()
        {
            TestObject.RegisterParser(TestOffset, TestParserDelegate);
            var retrieved = TestObject.RetrieveParser(TestOffset);
            retrieved(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DuplicateOffsetThrows()
        {
            TestObject.RegisterParser(TestOffset, TestParserDelegate);
            TestObject.RegisterParser(TestOffset, TestParserDelegate);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void NonRegisteredOffsetThrows()
        {
            TestObject.RetrieveParser(TestOffset);
        }

        [TestMethod]
        public void CanRegisterStart()
        {
            TestObject.RegisterStart(TestKey, TestOffset);
        }

        [TestMethod]
        public void CanRetrieveRegisteredOffset()
        {
            TestObject.RegisterStart(TestKey, TestOffset);
            uint retrieved = TestObject.RetrieveOffset(TestKey);
            Assert.AreEqual(TestOffset, retrieved);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DuplicateStartsThrows()
        {
            TestObject.RegisterStart(TestKey, TestOffset);
            TestObject.RegisterStart(TestKey, TestOffset + 1);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void NonRegisteredKeyThrows()
        {
            TestObject.RetrieveOffset(TestKey);
        }

        [TestMethod]
        public void CanRegisterEnd()
        {
            TestObject.RegisterStart(TestKey, TestOffset);
            TestObject.RegisterEnd(TestKey, TestOffset + 1);
        }
        
        [TestMethod]
        [ExpectedException(typeof(IncompleteOffsetException))]
        public void RegisterEndBeforeStartThrows()
        {
            TestObject.RegisterEnd(TestKey, TestOffset);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DuplicateEndsThrows()
        {
            TestObject.RegisterStart(TestKey, TestOffset);
            TestObject.RegisterEnd(TestKey, TestOffset + 1);
            TestObject.RegisterEnd(TestKey, TestOffset + 2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void EarlierEndThanStartThrows()
        {
            TestObject.RegisterStart(TestKey, TestOffset);
            TestObject.RegisterEnd(TestKey, TestOffset - 1);
        }

        [DataTestMethod]
        [DataRow((uint)0)]
        [DataRow((uint)1)]
        [DataRow((uint)13)]
        [DataRow((uint)100)]
        [DataRow((uint)10000)]
        public void CanRetrieveLength(uint expectedLength)
        {
            TestObject.RegisterStart(TestKey, TestOffset);
            TestObject.RegisterEnd(TestKey, TestOffset + expectedLength);
            Assert.AreEqual(expectedLength, TestObject.RetrieveLength(TestKey));
        }

        [TestMethod]
        [ExpectedException(typeof(IncompleteOffsetException))]
        public void LengthBeforeStartThrows()
        {
            TestObject.RetrieveLength(TestKey);
        }

        [TestMethod]
        [ExpectedException(typeof(IncompleteOffsetException))]
        public void LengthBeforeEndThrows()
        {
            TestObject.RegisterStart(TestKey, TestOffset);
            TestObject.RetrieveLength(TestKey);
        }
    }
}
