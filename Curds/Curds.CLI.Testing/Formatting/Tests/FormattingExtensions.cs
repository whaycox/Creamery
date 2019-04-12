using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Curds.Domain.CLI;

namespace Curds.CLI.Formatting.Tests
{
    using Tokens;

    [TestClass]
    public class FormattingExtensions : FormattingTemplate<Formatting.FormattedText>
    {
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
        public void AppendMany()
        {
            var test = TestObject.Append(Samples);
            test.Write(Writer);
            Assert.AreEqual(9, Writer.Writes.Count);
            Writer.StartsWith(NewLine(true))
                .ThenHas(NewLine(false))
                .ThenHas($"{nameof(Samples)}_1")
                .ThenHas($"{nameof(Samples)}_2")
                .ThenHas(Environment.NewLine)
                .ThenHas(NewLine(true))
                .ThenHas(NewLine(false))
                .ThenHas($"{nameof(Samples)}_3")
                .ThenHas($"{nameof(Samples)}_4");
        }

        [TestMethod]
        public void AppendLineManyInsertsNewLineBetweenEach()
        {
            var test = TestObject.AppendLine(Samples);
            test.Write(Writer);
            Assert.AreEqual(23, Writer.Writes.Count);
            Writer.StartsWith(NewLine(true))
                .ThenHas(NewLine(false))
                .ThenHas($"{nameof(Samples)}_1")
                .ThenHas(Environment.NewLine)
                .ThenHas(NewLine(true))
                .ThenHas(NewLine(false))
                .ThenHas($"{nameof(Samples)}_2")
                .ThenHas(Environment.NewLine)
                .ThenHas(NewLine(true))
                .ThenHas(NewLine(false))
                .ThenHas(Environment.NewLine)
                .ThenHas(NewLine(true))
                .ThenHas(NewLine(false))
                .ThenHas(Environment.NewLine)
                .ThenHas(NewLine(true))
                .ThenHas(NewLine(false))
                .ThenHas($"{nameof(Samples)}_3")
                .ThenHas(Environment.NewLine)
                .ThenHas(NewLine(true))
                .ThenHas(NewLine(false))
                .ThenHas($"{nameof(Samples)}_4")
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
                .ThenHas($"{CLIEnvironment.DefaultIndentation}{nameof(Indent)}")
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
                .ThenHas($"{CLIEnvironment.DefaultIndentation}{nameof(IndentLine)}")
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
                .ThenHas($"{CLIEnvironment.DefaultIndentation}{nameof(Samples)}_1")
                .ThenHas($"{nameof(Samples)}_2")
                .ThenHas(Environment.NewLine)
                .ThenHas(NewLine(true))
                .ThenHas(NewLine(false))
                .ThenHas($"{CLIEnvironment.DefaultIndentation}{nameof(Samples)}_3")
                .ThenHas($"{nameof(Samples)}_4")
                .ThenHas(Indents(0));
        }

        [TestMethod]
        public void IndentLineManyInsertsNewLineBetweenEach()
        {
            var test = TestObject.IndentLine(Samples);
            test.Write(Writer);
            Assert.AreEqual(25, Writer.Writes.Count);
            Writer.StartsWith(NewLine(true))
                .ThenHas(Indents(1))
                .ThenHas(NewLine(false))
                .ThenHas($"{CLIEnvironment.DefaultIndentation}{nameof(Samples)}_1")
                .ThenHas(Environment.NewLine)
                .ThenHas(NewLine(true))
                .ThenHas(NewLine(false))
                .ThenHas($"{CLIEnvironment.DefaultIndentation}{nameof(Samples)}_2")
                .ThenHas(Environment.NewLine)
                .ThenHas(NewLine(true))
                .ThenHas(NewLine(false))
                .ThenHas($"{CLIEnvironment.DefaultIndentation}{Environment.NewLine}")
                .ThenHas(NewLine(true))
                .ThenHas(NewLine(false))
                .ThenHas($"{CLIEnvironment.DefaultIndentation}{Environment.NewLine}")
                .ThenHas(NewLine(true))
                .ThenHas(NewLine(false))
                .ThenHas($"{CLIEnvironment.DefaultIndentation}{nameof(Samples)}_3")
                .ThenHas(Environment.NewLine)
                .ThenHas(NewLine(true))
                .ThenHas(NewLine(false))
                .ThenHas($"{CLIEnvironment.DefaultIndentation}{nameof(Samples)}_4")
                .ThenHas(Environment.NewLine)
                .ThenHas(NewLine(true))
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

        [TestMethod]
        public void ColorCanStack()
        {
            var test = TestObject.Color(ConsoleColor.Cyan, StackedColors());
            test.Write(Writer);
            Assert.AreEqual(9, Writer.Writes.Count);
            Writer.StartsWith(NewLine(true))
                .ThenHas(TextColor(ConsoleColor.Cyan))
                .ThenHas(NewLine(false))
                .ThenHas(CurrentColor)
                .ThenHas(TextColor(ConsoleColor.Green))
                .ThenHas(StackedColor)
                .ThenHas(TextColor(ConsoleColor.Cyan))
                .ThenHas(PreviousColor)
                .ThenHas(TextColor(CLIEnvironment.DefaultTextColor));
        }
        private Formatting.FormattedText StackedColors() => TestObject.Append(SampleToken(CurrentColor)).Color(ConsoleColor.Green, SampleToken(StackedColor)).Append(SampleToken(PreviousColor));
        private string CurrentColor => nameof(CurrentColor);
        private string StackedColor => nameof(StackedColor);
        private string PreviousColor => nameof(PreviousColor);

        private const string TestStart = "[ ";
        private const string TestEnd = " ]";
        private const string TestBetween = " | ";

        [TestMethod]
        public void ConcatenateThrowsWithoutOneElement()
        {
            Assert.ThrowsException<ArgumentNullException>(() => TestObject.Concatenate(TestStart, TestBetween, TestEnd, new BaseTextToken[0]));
            Assert.ThrowsException<ArgumentNullException>(() => TestObject.Concatenate(TestStart, TestBetween, TestEnd, null));
        }

        [TestMethod]
        public void NullStartIsEmpty()
        {
            var test = TestObject.Concatenate(null, TestBetween, TestEnd, Samples);
            test.Write(Writer);
            Assert.AreEqual(13, Writer.Writes.Count);
            Writer.StartsWith(NewLine(true))
                .ThenHas(NewLine(false))
                .ThenHas($"{nameof(Samples)}_1")
                .ThenHas(TestBetween)
                .ThenHas($"{nameof(Samples)}_2")
                .ThenHas(Environment.NewLine)
                .ThenHas(NewLine(true))
                .ThenHas(NewLine(false))
                .ThenHas(TestBetween)
                .ThenHas($"{nameof(Samples)}_3")
                .ThenHas(TestBetween)
                .ThenHas($"{nameof(Samples)}_4")
                .ThenHas(TestEnd);
        }

        [TestMethod]
        public void NullEndIsEmpty()
        {
            var test = TestObject.Concatenate(TestStart, TestBetween, null, Samples);
            test.Write(Writer);
            Assert.AreEqual(13, Writer.Writes.Count);
            Writer.StartsWith(NewLine(true))
                .ThenHas(NewLine(false))
                .ThenHas(TestStart)
                .ThenHas($"{nameof(Samples)}_1")
                .ThenHas(TestBetween)
                .ThenHas($"{nameof(Samples)}_2")
                .ThenHas(Environment.NewLine)
                .ThenHas(NewLine(true))
                .ThenHas(NewLine(false))
                .ThenHas(TestBetween)
                .ThenHas($"{nameof(Samples)}_3")
                .ThenHas(TestBetween)
                .ThenHas($"{nameof(Samples)}_4");
        }

        [TestMethod]
        public void NullBetweenIsEmpty()
        {
            var test = TestObject.Concatenate(TestStart, null, TestEnd, Samples);
            test.Write(Writer);
            Assert.AreEqual(11, Writer.Writes.Count);
            Writer.StartsWith(NewLine(true))
                .ThenHas(NewLine(false))
                .ThenHas(TestStart)
                .ThenHas($"{nameof(Samples)}_1")
                .ThenHas($"{nameof(Samples)}_2")
                .ThenHas(Environment.NewLine)
                .ThenHas(NewLine(true))
                .ThenHas(NewLine(false))
                .ThenHas($"{nameof(Samples)}_3")
                .ThenHas($"{nameof(Samples)}_4")
                .ThenHas(TestEnd);
        }

        [TestMethod]
        public void ConcatenateSurroundsProperly()
        {
            var test = TestObject.Concatenate(TestStart, TestBetween, TestEnd, Samples);
            test.Write(Writer);
            Assert.AreEqual(14, Writer.Writes.Count);
            Writer.StartsWith(NewLine(true))
                .ThenHas(NewLine(false))
                .ThenHas(TestStart)
                .ThenHas($"{nameof(Samples)}_1")
                .ThenHas(TestBetween)
                .ThenHas($"{nameof(Samples)}_2")
                .ThenHas(Environment.NewLine)
                .ThenHas(NewLine(true))
                .ThenHas(NewLine(false))
                .ThenHas(TestBetween)
                .ThenHas($"{nameof(Samples)}_3")
                .ThenHas(TestBetween)
                .ThenHas($"{nameof(Samples)}_4")
                .ThenHas(TestEnd);
        }
    }
}
