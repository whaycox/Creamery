using Curds.Domain;
using Curds.Domain.CLI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Curds.CLI.Writer.Tests
{
    using Formatting;
    using Formatting.Tokens;

    [TestClass]
    public class FormattingExtensions : TestTemplate
    {
        private MockConsoleWriter Writer = new MockConsoleWriter();
        private FormattedText Basic => FormattedText.New;

        private BaseTextToken SampleToken(string message) => PlainTextToken.Create(message);

        private IEnumerable<BaseTextToken> Samples => new List<BaseTextToken>
        {
            SampleToken($"{nameof(Samples)}_1"),
            SampleToken($"{nameof(Samples)}_2"),
            NewLineToken.New,
            SampleToken($"{nameof(Samples)}_3"),
            SampleToken($"{nameof(Samples)}_4"),
        };

        [TestMethod]
        public void Append()
        {
            FormattedText test = Basic.Append(SampleToken(nameof(Append)));
            test.Write(Writer);
            Assert.AreEqual(3, Writer.Writes.Count);
            Assert.AreEqual(Writer.StartOfNewLineWrite(true), Writer.Writes[0]);
            Assert.AreEqual(Writer.StartOfNewLineWrite(false), Writer.Writes[1]);
            Assert.AreEqual(nameof(Append), Writer.Writes[2]);
        }

        [TestMethod]
        public void AppendLine()
        {
            FormattedText test = Basic.AppendLine(SampleToken(nameof(AppendLine)));
            test.Write(Writer);
            Assert.AreEqual(5, Writer.Writes.Count);
            Assert.AreEqual(Writer.StartOfNewLineWrite(true), Writer.Writes[0]);
            Assert.AreEqual(Writer.StartOfNewLineWrite(false), Writer.Writes[1]);
            Assert.AreEqual(nameof(AppendLine), Writer.Writes[2]);
            Assert.AreEqual(Environment.NewLine, Writer.Writes[3]);
            Assert.AreEqual(Writer.StartOfNewLineWrite(true), Writer.Writes[4]);
        }

        [TestMethod]
        public void Indent()
        {
            FormattedText test = Basic.Indent(SampleToken(nameof(Indent)));
            test.Write(Writer);
            Assert.AreEqual(5, Writer.Writes.Count);
            Assert.AreEqual(Writer.StartOfNewLineWrite(true), Writer.Writes[0]);
            Assert.AreEqual(Writer.IndentsWrite(1), Writer.Writes[1]);
            Assert.AreEqual(Writer.StartOfNewLineWrite(false), Writer.Writes[2]);
            Assert.AreEqual($"\t{nameof(Indent)}", Writer.Writes[3]);
            Assert.AreEqual(Writer.IndentsWrite(0), Writer.Writes[4]);
        }

        [TestMethod]
        public void IndentLine()
        {
            FormattedText test = Basic.IndentLine(SampleToken(nameof(IndentLine)));
            test.Write(Writer);
            Assert.AreEqual(7, Writer.Writes.Count);
            Assert.AreEqual(Writer.StartOfNewLineWrite(true), Writer.Writes[0]);
            Assert.AreEqual(Writer.IndentsWrite(1), Writer.Writes[1]);
            Assert.AreEqual(Writer.StartOfNewLineWrite(false), Writer.Writes[2]);
            Assert.AreEqual($"\t{nameof(IndentLine)}", Writer.Writes[3]);
            Assert.AreEqual(Environment.NewLine, Writer.Writes[4]);
            Assert.AreEqual(Writer.StartOfNewLineWrite(true), Writer.Writes[5]);
            Assert.AreEqual(Writer.IndentsWrite(0), Writer.Writes[6]);
        }

        [TestMethod]
        public void IndentMany()
        {
            FormattedText test = Basic.Indent(Samples);
            test.Write(Writer);
            Assert.AreEqual(11, Writer.Writes.Count);
            Assert.AreEqual(Writer.StartOfNewLineWrite(true), Writer.Writes[0]);
            Assert.AreEqual(Writer.IndentsWrite(1), Writer.Writes[1]);
            Assert.AreEqual(Writer.StartOfNewLineWrite(false), Writer.Writes[2]);
            Assert.AreEqual($"\t{nameof(Samples)}_1", Writer.Writes[3]);
            Assert.AreEqual($"{nameof(Samples)}_2", Writer.Writes[4]);
            Assert.AreEqual(Environment.NewLine, Writer.Writes[5]);
            Assert.AreEqual(Writer.StartOfNewLineWrite(true), Writer.Writes[6]);
            Assert.AreEqual(Writer.StartOfNewLineWrite(false), Writer.Writes[7]);
            Assert.AreEqual($"\t{nameof(Samples)}_3", Writer.Writes[8]);
            Assert.AreEqual($"{nameof(Samples)}_4", Writer.Writes[9]);
            Assert.AreEqual(Writer.IndentsWrite(0), Writer.Writes[10]);
        }

        [TestMethod]
        public void Color()
        {
            FormattedText test = Basic.Color(ConsoleColor.Cyan, SampleToken(nameof(Color)));
            test.Write(Writer);
            Assert.AreEqual(5, Writer.Writes.Count);
            Assert.AreEqual(Writer.StartOfNewLineWrite(true), Writer.Writes[0]);
            Assert.AreEqual(Writer.TextColorChangeWrite(ConsoleColor.Cyan), Writer.Writes[1]);
            Assert.AreEqual(Writer.StartOfNewLineWrite(false), Writer.Writes[2]);
            Assert.AreEqual($"{nameof(Color)}", Writer.Writes[3]);
            Assert.AreEqual(Writer.TextColorChangeWrite(CLIEnvironment.DefaultTextColor), Writer.Writes[4]);
        }

        [TestMethod]
        public void ColorLine()
        {
            FormattedText test = Basic.ColorLine(ConsoleColor.Cyan, SampleToken(nameof(ColorLine)));
            test.Write(Writer);
            Assert.AreEqual(7, Writer.Writes.Count);
            Assert.AreEqual(Writer.StartOfNewLineWrite(true), Writer.Writes[0]);
            Assert.AreEqual(Writer.TextColorChangeWrite(ConsoleColor.Cyan), Writer.Writes[1]);
            Assert.AreEqual(Writer.StartOfNewLineWrite(false), Writer.Writes[2]);
            Assert.AreEqual($"{nameof(ColorLine)}", Writer.Writes[3]);
            Assert.AreEqual(Environment.NewLine, Writer.Writes[4]);
            Assert.AreEqual(Writer.StartOfNewLineWrite(true), Writer.Writes[5]);
            Assert.AreEqual(Writer.TextColorChangeWrite(CLIEnvironment.DefaultTextColor), Writer.Writes[6]);
        }

        [TestMethod]
        public void ColorMany()
        {
            FormattedText test = Basic.Color(ConsoleColor.Cyan, Samples);
            test.Write(Writer);
            Assert.AreEqual(11, Writer.Writes.Count);
            Assert.AreEqual(Writer.StartOfNewLineWrite(true), Writer.Writes[0]);
            Assert.AreEqual(Writer.TextColorChangeWrite(ConsoleColor.Cyan), Writer.Writes[1]);
            Assert.AreEqual(Writer.StartOfNewLineWrite(false), Writer.Writes[2]);
            Assert.AreEqual($"{nameof(Samples)}_1", Writer.Writes[3]);
            Assert.AreEqual($"{nameof(Samples)}_2", Writer.Writes[4]);
            Assert.AreEqual(Environment.NewLine, Writer.Writes[5]);
            Assert.AreEqual(Writer.StartOfNewLineWrite(true), Writer.Writes[6]);
            Assert.AreEqual(Writer.StartOfNewLineWrite(false), Writer.Writes[7]);
            Assert.AreEqual($"{nameof(Samples)}_3", Writer.Writes[8]);
            Assert.AreEqual($"{nameof(Samples)}_4", Writer.Writes[9]);
            Assert.AreEqual(Writer.TextColorChangeWrite(CLIEnvironment.DefaultTextColor), Writer.Writes[10]);
        }
    }
}
