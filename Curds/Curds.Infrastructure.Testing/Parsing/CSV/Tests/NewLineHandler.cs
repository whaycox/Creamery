using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Infrastructure.Parsing.CSV.Tests
{
    using Cases.NewLineHandler;

    [TestClass]
    public class NewLineHandler 
    {
        [TestMethod]
        public void IsNewLine()
        {
            TestCase(new LinuxLF());
            TestCase(new LinuxCRLF());
            TestCase(new WindowsLF());
            TestCase(new WindowsCRLF());
        }
        private void TestCase(NewLineHandlerCase testCase) => Assert.AreEqual(testCase.Expected, testCase.Handler.IsNewLine(testCase.First, testCase.Second));
    }
}
