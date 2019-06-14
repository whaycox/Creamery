using Curds;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace Gouda.Communication.Tests
{
    using Domain;
    using Enumerations;

    [TestClass]
    public class Parser : Test
    {
        private List<byte> Buffer = new List<byte>();

        private Domain.Parser BuildParser => new Domain.Parser(Buffer.ToArray());

        [TestMethod]
        public void CanParseBoolean()
        {
            Buffer
                .Append(true)
                .Append(false);

            Domain.Parser parser = BuildParser;
            Assert.IsTrue(parser.ParseBoolean());
            Assert.IsFalse(parser.ParseBoolean());
        }

        [TestMethod]
        public void CanParseInt()
        {
            Buffer.Append(TestInt);

            Domain.Parser parser = BuildParser;
            Assert.AreEqual(TestInt, parser.ParseInt());
        }
        private const int TestInt = 13;

        [TestMethod]
        public void CanParseLong()
        {
            Buffer.Append(TestLong);

            Domain.Parser parser = BuildParser;
            Assert.AreEqual(TestLong, parser.ParseLong());
        }
        private const long TestLong = 40;

        [TestMethod]
        public void CanParseString()
        {
            Buffer
                .Append(nameof(CanParseString))
                .Append(string.Empty)
                .Append(NullString)
                .Append(nameof(CanParseString));

            Domain.Parser parser = BuildParser;

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

            Domain.Parser parser = BuildParser;
            Assert.AreEqual(TestDate, parser.ParseDateTime());
        }
        private static readonly DateTimeOffset TestDate = new DateTimeOffset(2001, 1, 1, 1, 1, 1, TimeSpan.FromMinutes(37));

        [TestMethod]
        public void CanParseCommunicableType()
        {
            Buffer.Append(CommunicableType.Mock);

            Domain.Parser parser = BuildParser;
            Assert.AreEqual(CommunicableType.Mock, parser.ParseType());
        }

        [TestMethod]
        public void CanParseAcknowledgement()
        {
            Acknowledgement acknowledgement = new Acknowledgement(MockTime.Fetch);
            using (MemoryStream testStream = acknowledgement.ObjectStream() as MemoryStream)
                Buffer.AddRange(testStream.ToArray());

            Domain.Parser parser = BuildParser;
            Assert.AreEqual(CommunicableType.Acknowledgement, parser.ParseType());
            Acknowledgement parsed = parser.ParseObject(CommunicableType.Acknowledgement) as Acknowledgement;
            Assert.AreEqual(acknowledgement.Time, parsed.Time);
        }

        [TestMethod]
        public void CanParseError()
        {
            Error error = new Error(new Exception(nameof(CanParseError)));
            using (MemoryStream testStream = error.ObjectStream() as MemoryStream)
                Buffer.AddRange(testStream.ToArray());

            Domain.Parser parser = BuildParser;
            Assert.AreEqual(CommunicableType.Error, parser.ParseType());
            Error parsed = parser.ParseObject(CommunicableType.Error) as Error;
            Assert.AreEqual($"System.Exception: {nameof(CanParseError)}", parsed.ExceptionText);
        }
    }
}
