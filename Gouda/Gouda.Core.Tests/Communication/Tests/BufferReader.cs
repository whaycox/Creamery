using Curds;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace Gouda.Communication.Tests
{
    using Abstraction;
    using Check.Data.Domain;
    using Check.Domain;
    using Enumerations;

    [TestClass]
    public class BufferReader : Test
    {
        private List<byte> Buffer = new List<byte>();

        private Domain.BufferReader BuildParser => new Domain.BufferReader(Buffer.ToArray());

        [TestMethod]
        public void CanParseBoolean()
        {
            Buffer
                .Append(true)
                .Append(false);

            Domain.BufferReader parser = BuildParser;
            Assert.IsTrue(parser.ParseBoolean());
            Assert.IsFalse(parser.ParseBoolean());
        }

        [TestMethod]
        public void CanParseInt()
        {
            Buffer.Append(TestInt);

            Domain.BufferReader parser = BuildParser;
            Assert.AreEqual(TestInt, parser.ParseInt());
        }
        private const int TestInt = 13;

        [TestMethod]
        public void CanParseLong()
        {
            Buffer.Append(TestLong);

            Domain.BufferReader parser = BuildParser;
            Assert.AreEqual(TestLong, parser.ParseLong());
        }
        private const long TestLong = 40;

        [TestMethod]
        public void CanParseDecimal()
        {
            Buffer.Append(TestDecimal);

            Domain.BufferReader parser = BuildParser;
            Assert.AreEqual(TestDecimal, parser.ParseDecimal());
        }
        private const decimal TestDecimal = 987654321.0123456789m;

        [TestMethod]
        public void CanParseGuid()
        {
            Buffer.Append(Guid.Empty);

            Domain.BufferReader parser = BuildParser;
            Assert.AreEqual(Guid.Empty, parser.ParseGuid());
        }

        [TestMethod]
        public void CanParseString()
        {
            Buffer
                .Append(nameof(CanParseString))
                .Append(string.Empty)
                .Append(NullString)
                .Append(nameof(CanParseString));

            Domain.BufferReader parser = BuildParser;

            Assert.AreEqual(nameof(CanParseString), parser.ParseString());
            Assert.AreEqual(string.Empty, parser.ParseString());
            Assert.AreEqual(string.Empty, parser.ParseString());
            Assert.AreEqual(nameof(CanParseString), parser.ParseString());
        }
        private const string NullString = null;

        [TestMethod]
        public void CanParseDateTime()
        {
            Buffer.Append(TestDate);

            Domain.BufferReader parser = BuildParser;
            Assert.AreEqual(TestDate, parser.ParseDateTime());
        }
        private static readonly DateTimeOffset TestDate = new DateTimeOffset(2001, 1, 1, 1, 1, 1, TimeSpan.FromMinutes(37));

        [TestMethod]
        public void CanParseDictionary()
        {
            Buffer.Append(TestDictionary, (b, k) => b.Append(k), (b, v) => b.Append(v));

            Domain.BufferReader parser = BuildParser;
            Dictionary<int, string> parsed = parser.ParseDictionary((p) => p.ParseInt(), (p) => p.ParseString());
            Assert.AreEqual(ExpectedPairs, parsed.Count);
            for (int i = 1; i <= ExpectedPairs; i++)
            {
                Assert.IsTrue(parsed.ContainsKey(i));
                if (i % 2 == 0)
                    Assert.AreEqual(nameof(CanParseDictionary), parsed[i]);
                else
                    Assert.AreEqual(nameof(TestDictionary), parsed[i]);
            }
        }
        private Dictionary<int, string> TestDictionary => new Dictionary<int, string>
        {
            { 1, nameof(TestDictionary) },
            { 2, nameof(CanParseDictionary) },
            { 3, nameof(TestDictionary) },
            { 4, nameof(CanParseDictionary) },
            { 5, nameof(TestDictionary) },
        };
        private const int ExpectedPairs = 5;

        [TestMethod]
        public void CanParseCommunicableType()
        {
            Buffer.Append(CommunicableType.Mock);

            Domain.BufferReader parser = BuildParser;
            Assert.AreEqual(CommunicableType.Mock, parser.ParseType());
        }
    }
}
