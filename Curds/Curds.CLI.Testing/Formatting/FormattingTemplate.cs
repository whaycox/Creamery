using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain;
using Curds.Domain.CLI;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.CLI.Formatting
{
    public abstract class FormattingTemplate<T> : TestTemplate<T>
    {
        protected MockConsoleWriter Writer = new MockConsoleWriter();

        protected string NewLine(bool newValue) => MockConsoleWriter.StartOfNewLineWrite(newValue);
        protected string TextColor(ConsoleColor textColor) => MockConsoleWriter.TextColorChangeWrite(textColor);
        protected string Indents(int indents) => MockConsoleWriter.IndentsWrite(indents);
        protected string EnvironmentExit(int exitCode) => MockConsoleWriter.EnvironmentExit(exitCode);
    }

    public static class FormattingTemplateExtensions
    {
        private static void Compare((MockConsoleWriter writer, int index) pair, string expected) => Assert.AreEqual(expected, pair.writer.Writes[pair.index]);

        public static (MockConsoleWriter writer, int index) StartsWith(this MockConsoleWriter writer, string expected)
        {
            (MockConsoleWriter writer, int index) pair = (writer, 0);
            Compare(pair, expected);
            pair.index++;
            return pair;
        }

        public static (MockConsoleWriter writer, int index) ThenHas(this (MockConsoleWriter writer, int index) pair, string expected)
        {
            Compare(pair, expected);
            pair.index++;
            return pair;
        }

        public static void EndsWith(this MockConsoleWriter writer, string expected) => Assert.AreEqual(writer.Writes[writer.Writes.Count - 1], expected);
    }
}
