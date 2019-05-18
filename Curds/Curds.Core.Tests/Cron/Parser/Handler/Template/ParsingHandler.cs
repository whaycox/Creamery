using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Curds.Cron.Parser.Handler.Template
{
    public abstract class ParsingHandler<T> : Test<T> where T : Domain.ParsingHandler
    {
        private Mock.ParsingHandler MockHandler = new Mock.ParsingHandler();

        protected abstract T Build(Domain.ParsingHandler successor);

        protected void TestParse<U>(string range, Action<U> testDelegate) where U : Range.Domain.Basic
        {
            var parsed = TestObject.HandleParse(range);
            Assert.IsInstanceOfType(parsed, typeof(U));
            testDelegate(parsed as U);
        }
        protected void DoNothing<U>(U parsed) where U : Range.Domain.Basic { }

        [TestMethod]
        public void EndOfChainThrows()
        {
            T handler = Build(null);
            Assert.ThrowsException<FormatException>(() => handler.HandleParse(nameof(EndOfChainThrows)));
        }

        [TestMethod]
        public void PassesUnknownRequestToSuccessor()
        {
            T parser = Build(MockHandler);
            parser.HandleParse(nameof(PassesUnknownRequestToSuccessor));
            Assert.AreEqual(1, MockHandler.RangesHandled.Count);
            Assert.AreEqual(nameof(PassesUnknownRequestToSuccessor), MockHandler.RangesHandled[0]);
        }
    }
}
