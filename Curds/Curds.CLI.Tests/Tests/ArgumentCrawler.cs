using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Curds.CLI.Tests
{
    [TestClass]
    public class ArgumentCrawler : Test<Implementation.ArgumentCrawler>
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
        protected override Implementation.ArgumentCrawler TestObject { get; } = new Implementation.ArgumentCrawler(Arguments);

        [TestMethod]
        public void EmptyArgumentsThrows()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new Implementation.ArgumentCrawler(null));
            Assert.ThrowsException<ArgumentNullException>(() => new Implementation.ArgumentCrawler(new string[0]));
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
