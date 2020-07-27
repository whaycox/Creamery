using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Curds.Text.Tests
{
    using Implementation;

    [TestClass]
    public class IndentStringBuilderTest
    {
        private string TestString = nameof(TestString);

        private IndentStringBuilder TestObject = new IndentStringBuilder();

        [TestMethod]
        public void SettingIndentsNegativeGoesToZero()
        {
            TestObject.Indents--;

            Assert.AreEqual(0, TestObject.Indents);
        }

        [TestMethod]
        public void CanAppendText()
        {
            TestObject.Append(TestString);
        }

        [TestMethod]
        public void AppendedTextIsFlushed()
        {
            TestObject.Append(TestString);

            string actual = TestObject.Flush();

            Assert.AreEqual(TestString, actual);
        }

        [TestMethod]
        public void SubsequentFlushIsEmpty()
        {
            TestObject.Append(TestString);
            TestObject.Flush();

            string actual = TestObject.Flush();

            Assert.AreEqual(string.Empty, actual);
        }

        [TestMethod]
        public void IncreaseIndentBeforeAppendPrependsIndent()
        {
            TestObject.Indents++;
            TestObject.Append(TestString);

            string actual = TestObject.Flush();

            Assert.AreEqual($"\t{TestString}", actual);
        }

        [TestMethod]
        public void CanAppendLine()
        {
            TestObject.AppendLine(TestString);
        }

        [TestMethod]
        public void AppendLineAppendsNewLine()
        {
            TestObject.AppendLine(TestString);

            string actual = TestObject.Flush();

            Assert.AreEqual($"{TestString}{Environment.NewLine}", actual);
        }

        [TestMethod]
        public void TextAppendedAfterNewLineIsIndented()
        {
            TestObject.AppendLine(TestString);
            TestObject.Indents++;
            TestObject.Append(TestString);

            string actual = TestObject.Flush();

            Assert.AreEqual($"{TestString}{Environment.NewLine}\t{TestString}", actual);
        }

        [TestMethod]
        public void TextAppendedWithNewLineGetsIndented()
        {
            TestObject.Indents++;
            TestObject.Append($"{TestString}{Environment.NewLine}{TestString}");

            string actual = TestObject.Flush();

            Assert.AreEqual($"\t{TestString}{Environment.NewLine}\t{TestString}", actual);
        }

        [TestMethod]
        public void TextAppendLineWithNewLineGetsIndented()
        {
            TestObject.Indents++;
            TestObject.AppendLine($"{TestString}{Environment.NewLine}{TestString}");

            string actual = TestObject.Flush();

            Assert.AreEqual($"\t{TestString}{Environment.NewLine}\t{TestString}{Environment.NewLine}", actual);
        }

        [TestMethod]
        public void CanUseIndentScope()
        {
            using (TestObject.CreateIndentScope())
                TestObject.Append(TestString);

            string actual = TestObject.Flush();

            Assert.AreEqual($"\t{TestString}", actual);
        }

        [TestMethod]
        public void IndentScopeRevertsIndentsWhenDisposed()
        {
            using (TestObject.CreateIndentScope())
                TestObject.AppendLine(TestString);
            TestObject.Append(TestString);

            string actual = TestObject.Flush();

            Assert.AreEqual($"\t{TestString}{Environment.NewLine}{TestString}", actual);
        }
    }
}
