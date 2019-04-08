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
        public void FullyConsumedIsFalseAtStart()
        {
            Assert.IsFalse(TestObject.FullyConsumed);
        }

        [TestMethod]
        public void StepBackwardsThrowsAtStart()
        {
            Assert.ThrowsException<InvalidOperationException>(() => TestObject.StepBackwards());
        }

        [TestMethod]
        public void FullyConsumedIsTrueOnlyAtEnd()
        {
            Assert.IsFalse(TestObject.FullyConsumed);
            TestObject.Consume();
            Assert.IsFalse(TestObject.FullyConsumed);
            TestObject.Consume();
            Assert.IsFalse(TestObject.FullyConsumed);
            TestObject.Consume();
            Assert.IsTrue(TestObject.FullyConsumed);
        }

        [TestMethod]
        public void ConsumesCorrectly()
        {
            Assert.AreEqual(One, TestObject.Consume());
            Assert.AreEqual(Two, TestObject.Consume());
            Assert.AreEqual(Three, TestObject.Consume());
        }

        [TestMethod]
        public void StepsBackwardCorrectly()
        {
            for (int i = 0; i < 3; i++)
                TestObject.Consume();

            TestObject.StepBackwards();
            Assert.AreEqual(Three, TestObject.Consume());

            TestObject.StepBackwards();
            TestObject.StepBackwards();
            Assert.AreEqual(Two, TestObject.Consume());
        }

        [TestMethod]
        public void CannotConsumeAfterEnd()
        {
            TestObject.Consume();
            TestObject.Consume();
            TestObject.Consume();
            Assert.ThrowsException<InvalidOperationException>(() => TestObject.Consume());
        }

    }
}
