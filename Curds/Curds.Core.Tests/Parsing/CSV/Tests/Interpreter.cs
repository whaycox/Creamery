using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text;

namespace Curds.Parsing.CSV.Tests
{
    using Abstraction;

    [TestClass]
    public class Interpreter : Test
    {
        private static Stream TextStream(string testString) => new MemoryStream(DefaultEncoding.GetBytes(testString));
        private static Encoding DefaultEncoding = Encoding.UTF8;
        private const string TestString = "The,\"quick\",brown\r\nfox,\"jumps\"\r\nover,the,lazy,dog";

        private ICSVOptions DefaultOptions = new Domain.CSVOptions();

        private void TestWithString(string testString, Action<Implementation.Interpreter> testDelegate)
        {
            using (Stream testStream = TextStream(testString))
            {
                Implementation.Interpreter interpreter = new Implementation.Interpreter(testStream, DefaultOptions);
                testDelegate(interpreter);
            }
        }
        private void TestWithTestingString(Action<Implementation.Interpreter> testDelegate) => TestWithString(TestString, testDelegate);

        [TestMethod]
        public void KnowsWhatCurrentCharIs() => TestWithTestingString(KnowsWhatCurrentCharIsHelper);
        private void KnowsWhatCurrentCharIsHelper(Implementation.Interpreter interpreter)
        {
            Assert.AreEqual("T", interpreter.Current);
            interpreter.Read();
            Assert.AreEqual("h", interpreter.Current);
            interpreter.Read();
            Assert.AreEqual("e", interpreter.Current);
        }

        [TestMethod]
        public void KnowsWhatNextCharIs() => TestWithTestingString(KnowsWhatNextCharIsHelper);
        private void KnowsWhatNextCharIsHelper(Implementation.Interpreter interpreter)
        {
            Assert.AreEqual("h", interpreter.Next);
            interpreter.Read();
            Assert.AreEqual("e", interpreter.Next);
            interpreter.Read();
            Assert.AreEqual(",", interpreter.Next);
        }

        [TestMethod]
        public void KnowsWhenAtSeparator() => TestWithTestingString(KnowsWhenAtSeparatorHelper);
        private void KnowsWhenAtSeparatorHelper(Implementation.Interpreter interpreter)
        {
            Assert.IsFalse(interpreter.IsAtSeparator);
            interpreter.Read();
            Assert.IsFalse(interpreter.IsAtSeparator);
            interpreter.Read();
            Assert.IsFalse(interpreter.IsAtSeparator);
            interpreter.Read();
            Assert.IsTrue(interpreter.IsAtSeparator);
        }

        [TestMethod]
        public void KnowsWhenNextIsSeparator() => TestWithTestingString(KnowsWhenNextIsSeparatorHelper);
        private void KnowsWhenNextIsSeparatorHelper(Implementation.Interpreter interpreter)
        {
            Assert.IsFalse(interpreter.NextIsSeparator);
            interpreter.Read();
            Assert.IsFalse(interpreter.NextIsSeparator);
            interpreter.Read();
            Assert.IsTrue(interpreter.NextIsSeparator);
            interpreter.Read();
            Assert.IsFalse(interpreter.NextIsSeparator);
        }

        [TestMethod]
        public void KnowsWhenAtQualifier() => TestWithTestingString(KnowsWhenAtQualifierHelper);
        private void KnowsWhenAtQualifierHelper(Implementation.Interpreter interpreter)
        {
            Assert.IsFalse(interpreter.IsAtQualifier);
            interpreter.Read();
            Assert.IsFalse(interpreter.IsAtQualifier);
            interpreter.Read();
            Assert.IsFalse(interpreter.IsAtQualifier);
            interpreter.Read();
            Assert.IsFalse(interpreter.IsAtQualifier);
            interpreter.Read();
            Assert.IsTrue(interpreter.IsAtQualifier);
        }

        [TestMethod]
        public void KnowsWhenNextIsQualifier() => TestWithTestingString(KnowsWhenNextIsQualifierHelper);
        private void KnowsWhenNextIsQualifierHelper(Implementation.Interpreter interpreter)
        {
            Assert.IsFalse(interpreter.NextIsQualifier);
            interpreter.Read();
            Assert.IsFalse(interpreter.NextIsQualifier);
            interpreter.Read();
            Assert.IsFalse(interpreter.NextIsQualifier);
            interpreter.Read();
            Assert.IsTrue(interpreter.NextIsQualifier);
        }

        [TestMethod]
        public void KnowsWhenAtNewLine() => TestWithTestingString(KnowsWhenAtNewLineHelper);
        private void KnowsWhenAtNewLineHelper(Implementation.Interpreter interpreter)
        {
            for (int i = 0; i < 15; i++)
                interpreter.Read();

            Assert.IsFalse(interpreter.IsAtNewLine);
            interpreter.Read();
            Assert.IsFalse(interpreter.IsAtNewLine);
            interpreter.Read();
            Assert.IsTrue(interpreter.IsAtNewLine);
        }

        [TestMethod]
        public void KnowsWhenNextIsNewLine() => TestWithTestingString(KnowsWhenNextIsNewLineHelper);
        private void KnowsWhenNextIsNewLineHelper(Implementation.Interpreter interpreter)
        {
            for (int i = 0; i < 15; i++)
                interpreter.Read();

            Assert.IsFalse(interpreter.NextIsNewLine);
            interpreter.Read();
            Assert.IsTrue(interpreter.NextIsNewLine);
            interpreter.Read();
            Assert.IsFalse(interpreter.NextIsNewLine);
        }

        [TestMethod]
        public void CanReadEntireString() => TestWithTestingString(CanReadEntireStringHelper);
        private void CanReadEntireStringHelper(Implementation.Interpreter interpreter)
        {
            StringBuilder actual = new StringBuilder();
            while (!interpreter.IsEmpty)
                actual.Append(interpreter.Read());
            Assert.AreEqual(TestString, actual.ToString());
        }

        [TestMethod]
        public void KnowsWhenAtEscapedQualifier() => TestWithString(EscapedQualifierString, KnowsWhenAtEscapedQualifierHelper);
        private const string EscapedQualifierString = "\"ABC\"\"DE";
        private void KnowsWhenAtEscapedQualifierHelper(Implementation.Interpreter interpreter)
        {
            for (int i = 0; i < 4; i++)
            {
                Assert.IsFalse(interpreter.IsAtEscapedQualifier);
                interpreter.Read();
            }
            Assert.IsTrue(interpreter.IsAtEscapedQualifier);
        }

        [TestMethod]
        public void ConsumesBothEscapeAndQualifier() => TestWithString(EscapedQualifierString, ConsumesBothEscapeAndQualifierHelper);
        private void ConsumesBothEscapeAndQualifierHelper(Implementation.Interpreter interpreter)
        {
            for (int i = 0; i < 4; i++)
                interpreter.Read();
            Assert.IsTrue(interpreter.IsAtEscapedQualifier);
            Assert.AreEqual("\"", interpreter.Read());
            Assert.IsFalse(interpreter.IsAtEscapedQualifier);
        }

        [TestMethod]
        public void KnowsAMidstreamQualifiedEnding() => TestWithString(MidstreamQualifiedEndingString, KnowsAMidstreamQualifiedEndingHelper);
        private const string MidstreamQualifiedEndingString = "\"A\",oeu";
        private void KnowsAMidstreamQualifiedEndingHelper(Implementation.Interpreter interpreter)
        {
            for (int i = 0; i < 2; i++)
            {
                Assert.IsFalse(interpreter.IsAtQualifiedEnding);
                interpreter.Read();
            }
            Assert.IsTrue(interpreter.IsAtQualifiedEnding);
        }

        [TestMethod]
        public void KnowsANewLineQualifiedEnding() => TestWithString(NewLineQualifiedEndingString, KnowsANewLineQualifiedEndingHelper);
        private const string NewLineQualifiedEndingString = "\"A\"\r\noeu";
        private void KnowsANewLineQualifiedEndingHelper(Implementation.Interpreter interpreter)
        {
            for (int i = 0; i < 2; i++)
            {
                Assert.IsFalse(interpreter.IsAtQualifiedEnding);
                interpreter.Read();
            }
            Assert.IsTrue(interpreter.IsAtQualifiedEnding);
        }

        [TestMethod]
        public void KnowsAnEndOfStringQualifiedEnding() => TestWithString(EndOfStringQualifiedEndingString, KnowsAnEndOfStringQualifiedEndingHelper);
        private const string EndOfStringQualifiedEndingString = "\"A\"";
        private void KnowsAnEndOfStringQualifiedEndingHelper(Implementation.Interpreter interpreter)
        {
            for (int i = 0; i < 2; i++)
            {
                Assert.IsFalse(interpreter.IsAtQualifiedEnding);
                interpreter.Read();
            }
            Assert.IsTrue(interpreter.IsAtQualifiedEnding);
        }
    }
}
