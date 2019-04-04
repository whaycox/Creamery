using Curds.Domain.CLI.Formatting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Curds.Domain.CLI;
using System;

namespace Curds.CLI.Formatting.Tests
{
    [TestClass]
    public class FormattedText : FormattingTemplate<Formatting.FormattedText>
    {
        private static BaseTextToken Token => new MockTextToken();
        private Formatting.FormattedText Sample => Formatting.FormattedText.New
            .Append(Token)
            .Append(Token)
            .Append(Token);

        protected override Formatting.FormattedText TestObject { get; } = Formatting.FormattedText.New;

        [TestMethod]
        public void EmptyConstructors()
        {
            var test = new Formatting.FormattedText();
            Assert.AreEqual(0, test.Output.Count());

            test = new Formatting.FormattedText(text: null);
            Assert.AreEqual(0, test.Output.Count());
        }

        [TestMethod]
        public void StartsWithManyTokens()
        {
            var test = new Formatting.FormattedText(Sample);
            Assert.AreEqual(3, test.Output.Count());

            test = new Formatting.FormattedText(test);
            Assert.AreEqual(3, test.Output.Count());
        }

        [TestMethod]
        public void CanAdd()
        {
            TestObject.Add(Token);
            Assert.AreEqual(1, TestObject.Output.Count());
        }

        [TestMethod]
        public void CanAddText()
        {
            TestObject.Add(Sample);
            Assert.AreEqual(3, TestObject.Output.Count());
        }

        [TestMethod]
        public void AddLineAddsExtraToken()
        {
            TestObject.AddLine(Token);
            var results = TestObject.Output.ToArray();
            Assert.AreEqual(2, results.Length);
            Assert.IsTrue(results[1] is Tokens.NewLineToken);
        }

        [TestMethod]
        public void AddLineAppendsTokenToText()
        {
            TestObject.AddLine(Sample);
            var results = TestObject.Output.ToArray();
            Assert.AreEqual(4, results.Length);
            Assert.IsTrue(results[3] is Tokens.NewLineToken);
        }

        [TestMethod]
        public void WriteOutputsEachTokenInOrder()
        {
            TestObject.Add(Token);
            TestObject.AddLine(Token);
            TestObject.Add(Token);
            TestObject.Write(Writer);

            Assert.AreEqual(8, Writer.Writes.Count);
            Writer.StartsWith(NewLine(true))
                .ThenHas(NewLine(false))
                .ThenHas(MockTextToken.Output)
                .ThenHas(MockTextToken.Output)
                .ThenHas(Environment.NewLine)
                .ThenHas(NewLine(true))
                .ThenHas(NewLine(false))
                .ThenHas(MockTextToken.Output);
        }
    }
}
