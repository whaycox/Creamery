using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Parsing.Reader.Tests
{
    [TestClass]
    public class NewLineHandler : Test
    {
        private void TestWindowsNewLine(Domain.NewLineHandler handler, bool expected)
        {
            bool actual = handler.IsNewLine(Domain.NewLineHandler.CR, Domain.NewLineHandler.LF);
            Assert.AreEqual(expected, actual);
        }

        private void TestLinuxNewLine(Domain.NewLineHandler handler, bool expected)
        {
            bool actual = handler.IsNewLine(Domain.NewLineHandler.LF, null);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WindowsNewLines()
        {
            var handler = Domain.NewLineHandler.Windows;
            TestWindowsNewLine(handler, true);
            TestLinuxNewLine(handler, false);
        }

        [TestMethod]
        public void LinuxNewLines()
        {
            var handler = Domain.NewLineHandler.Linux;
            TestWindowsNewLine(handler, false);
            TestLinuxNewLine(handler, true);
        }
    }
}
