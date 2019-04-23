using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Curds.Domain;

namespace Curds.CLI.Writer.Tests
{
    [TestClass]
    public class ConsoleWriter : IConsoleWriterTemplate<Writer.ConsoleWriter>
    {
        private Writer.ConsoleWriter _obj = new Writer.ConsoleWriter();
        protected override Writer.ConsoleWriter TestObject => _obj;
    }
}
