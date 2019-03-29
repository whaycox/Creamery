using Curds.Domain.CLI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Curds.CLI.Formatting.Tests
{
    using Tokens;

    [TestClass]
    public class FormattingExtensions : FormattingTemplate<Formatting.FormattedText>
    {
        private MockConsoleWriter Writer = new MockConsoleWriter();

        private BaseTextToken SampleToken(string message) => PlainTextToken.Create(message);

        private IEnumerable<BaseTextToken> Samples => new List<BaseTextToken>
        {
            SampleToken($"{nameof(Samples)}_1"),
            SampleToken($"{nameof(Samples)}_2"),
            NewLineToken.New,
            SampleToken($"{nameof(Samples)}_3"),
            SampleToken($"{nameof(Samples)}_4"),
        };

        protected override Formatting.FormattedText TestObject => Formatting.FormattedText.New;

        [TestMethod]
        public void Append()
        {
            var test = TestObject.Append(SampleToken(nameof(Append)));
            test.Write(Writer);
            Assert.AreEqual(3, Writer.Writes.Count);
            Writer.StartsWith(NewLine(true))
                .ThenHas(NewLine(false))
                .ThenHas(nameof(Append));
        }

        [TestMethod]
        public void AppendLine()
        {
            var test = TestObject.AppendLine(SampleToken(nameof(AppendLine)));
            test.Write(Writer);
            Assert.AreEqual(5, Writer.Writes.Count);
            Writer.StartsWith(NewLine(true))
                .ThenHas(NewLine(false))
                .ThenHas(nameof(AppendLine))
                .ThenHas(Environment.NewLine)
                .ThenHas(NewLine(true));
        }

        [TestMethod]
        public void Indent()
        {
            var test = TestObject.Indent(SampleToken(nameof(Indent)));
            test.Write(Writer);
            Assert.AreEqual(5, Writer.Writes.Count);
            Writer.StartsWith(NewLine(true))
                .ThenHas(Indents(1))
                .ThenHas(NewLine(false))
                .ThenHas($"\t{nameof(Indent)}")
                .ThenHas(Indents(0));
        }

        [TestMethod]
        public void IndentLine()
        {
            var test = TestObject.IndentLine(SampleToken(nameof(IndentLine)));
            test.Write(Writer);
            Assert.AreEqual(7, Writer.Writes.Count);
            Writer.StartsWith(NewLine(true))
                .ThenHas(Indents(1))
                .ThenHas(NewLine(false))
                .ThenHas($"\t{nameof(IndentLine)}")
                .ThenHas(Environment.NewLine)
                .ThenHas(NewLine(true))
                .ThenHas(Indents(0));
        }

        [TestMethod]
        public void IndentMany()
        {
            var test = TestObject.Indent(Samples);
            test.Write(Writer);
            Assert.AreEqual(11, Writer.Writes.Count);
            Writer.StartsWith(NewLine(true))
                .ThenHas(Indents(1))
                .ThenHas(NewLine(false))
                .ThenHas($"\t{nameof(Samples)}_1")
                .ThenHas($"{nameof(Samples)}_2")
                .ThenHas(Environment.NewLine)
                .ThenHas(NewLine(true))
                .ThenHas(NewLine(false))
                .ThenHas($"\t{nameof(Samples)}_3")
                .ThenHas($"{nameof(Samples)}_4")
                .ThenHas(Indents(0));
        }

        [TestMethod]
        public void Color()
        {
            var test = TestObject.Color(ConsoleColor.Cyan, SampleToken(nameof(Color)));
            test.Write(Writer);
            Assert.AreEqual(5, Writer.Writes.Count);
            Writer.StartsWith(NewLine(true))
                .ThenHas(TextColor(ConsoleColor.Cyan))
                .ThenHas(NewLine(false))
                .ThenHas(nameof(Color))
                .ThenHas(TextColor(CLIEnvironment.DefaultTextColor));
        }

        [TestMethod]
        public void ColorLine()
        {
            var test = TestObject.ColorLine(ConsoleColor.Cyan, SampleToken(nameof(ColorLine)));
            test.Write(Writer);
            Assert.AreEqual(7, Writer.Writes.Count);
            Writer.StartsWith(NewLine(true))
                .ThenHas(TextColor(ConsoleColor.Cyan))
                .ThenHas(NewLine(false))
                .ThenHas(nameof(ColorLine))
                .ThenHas(Environment.NewLine)
                .ThenHas(NewLine(true))
                .ThenHas(TextColor(CLIEnvironment.DefaultTextColor));
        }

        [TestMethod]
        public void ColorMany()
        {
            var test = TestObject.Color(ConsoleColor.Cyan, Samples);
            test.Write(Writer);
            Assert.AreEqual(11, Writer.Writes.Count);
            Writer.StartsWith(NewLine(true))
                .ThenHas(TextColor(ConsoleColor.Cyan))
                .ThenHas(NewLine(false))
                .ThenHas($"{nameof(Samples)}_1")
                .ThenHas($"{nameof(Samples)}_2")
                .ThenHas(Environment.NewLine)
                .ThenHas(NewLine(true))
                .ThenHas(NewLine(false))
                .ThenHas($"{nameof(Samples)}_3")
                .ThenHas($"{nameof(Samples)}_4")
                .ThenHas(TextColor(CLIEnvironment.DefaultTextColor));
        }
    }
}
