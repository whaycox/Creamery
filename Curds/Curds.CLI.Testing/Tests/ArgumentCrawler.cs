using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Curds.Domain;

namespace Curds.CLI.Tests
{
    [TestClass]
    public class ArgumentCrawler : TestTemplate<CLI.ArgumentCrawler>
    {
        private const string One = nameof(One);
        private const string Two = nameof(Two);
        private const string Three = nameof(Three);

        private static string[] Arguments => new string[]
        {
            One,
            Two,
            Three,
        };
        protected override CLI.ArgumentCrawler TestObject { get; } = new CLI.ArgumentCrawler(Arguments);

        [TestMethod]
        public void EmptyArgumentsThrows()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new CLI.ArgumentCrawler(null));
            Assert.ThrowsException<ArgumentNullException>(() => new CLI.ArgumentCrawler(new string[0]));
        }

        [TestMethod]
        public void AtStartIsTrueOnlyAtStart()
        {
            Assert.IsTrue(TestObject.AtStart);
            TestObject.Next();
            Assert.IsFalse(TestObject.AtStart);
            TestObject.Next();
            Assert.IsFalse(TestObject.AtStart);
        }

        [TestMethod]
        public void AtEndIsTrueOnlyAtEnd()
        {
            Assert.IsFalse(TestObject.AtEnd);
            TestObject.Next();
            Assert.IsFalse(TestObject.AtEnd);
            TestObject.Next();
            Assert.IsTrue(TestObject.AtEnd);
        }

        [TestMethod]
        public void CrawlsCorrectly()
        {
            Assert.AreEqual(One, TestObject.Parse());
            TestObject.Next();
            Assert.AreEqual(Two, TestObject.Parse());
            TestObject.Next();
            Assert.AreEqual(Three, TestObject.Parse());
            TestObject.Previous();
            Assert.AreEqual(Two, TestObject.Parse());
            TestObject.Previous();
            Assert.AreEqual(One, TestObject.Parse());
        }

        [TestMethod]
        public void CannotCrawlBeforeStart()
        {
            Assert.ThrowsException<InvalidOperationException>(() => TestObject.Previous());
        }

        [TestMethod]
        public void CannotCrawlAfterEnd()
        {
            TestObject.Next();
            TestObject.Next();
            Assert.ThrowsException<InvalidOperationException>(() => TestObject.Next());
        }

    }
}
