using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Curds.CLI
{
    public abstract class IConsoleWriterTemplate<T> : TestTemplate<T> where T : IConsoleWriter
    {
        protected StringWriter ConsoleOutput = null;
        protected string OutputResults => ConsoleOutput.ToString();

        [TestInitialize]
        public void CaptureConsoleOutput()
        {
            ConsoleOutput = new StringWriter();
            Console.SetOut(ConsoleOutput);
        }

        [TestCleanup]
        public void Cleanup()
        {
            ConsoleOutput.Dispose();
        }
        
        [TestMethod]
        public void StartsWithDefaults()
        {
            Assert.AreEqual(CLIEnvironment.DefaultIndentation, TestObject.Indentation);
            Assert.IsTrue(TestObject.StartOfNewLine);
        }

        [TestMethod]
        public void WriteOutputsToConsole()
        {
            TestObject.Write(nameof(WriteOutputsToConsole));
            Assert.AreEqual(nameof(WriteOutputsToConsole), OutputResults);
        }

        [TestMethod]
        public void AfterWriteNotAtStartOfNewLine()
        {
            TestObject.Write(nameof(AfterWriteNotAtStartOfNewLine));
            Assert.IsFalse(TestObject.StartOfNewLine);
            TestObject.Write(Environment.NewLine);
            Assert.IsFalse(TestObject.StartOfNewLine);
        }

        [TestMethod]
        public void IndentsOnlyIncludedAtStartOfNewLine()
        {
            TestObject.Indents++;
            TestObject.Write(nameof(IndentsOnlyIncludedAtStartOfNewLine));
            TestObject.Write(Environment.NewLine);
            TestObject.Write(nameof(IndentsOnlyIncludedAtStartOfNewLine));

            Assert.AreEqual(ExpectedOutput, OutputResults);
        }
        private string ExpectedOutput => $"{CLIEnvironment.DefaultIndentation}{nameof(IndentsOnlyIncludedAtStartOfNewLine)}{Environment.NewLine}{nameof(IndentsOnlyIncludedAtStartOfNewLine)}";

        [TestMethod]
        public void SetTextColorChangesCosoleColor()
        {
            TestObject.SetTextColor(ConsoleColor.Cyan);
            Assert.AreEqual(ConsoleColor.Cyan, Console.ForegroundColor);
        }

        [TestMethod]
        public void ResetTextColorResetsConsoleColor()
        {
            TestObject.SetTextColor(ConsoleColor.Cyan);
            TestObject.ResetTextColor();
            Assert.AreEqual(CLIEnvironment.DefaultTextColor, Console.ForegroundColor);
        }

    }
}
