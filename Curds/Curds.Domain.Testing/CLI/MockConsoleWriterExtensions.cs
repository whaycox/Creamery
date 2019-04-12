using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Domain.CLI
{
    public static class MockConsoleWriterExtensions
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
