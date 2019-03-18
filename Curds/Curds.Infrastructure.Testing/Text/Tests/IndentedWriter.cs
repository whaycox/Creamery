using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Curds.Application.Text;

namespace Curds.Infrastructure.Text.Tests
{
    [TestClass]
    public class IndentedWriter : IIndentedWriterTemplate<Text.IndentedWriter>
    {
        private Text.IndentedWriter _obj = new Text.IndentedWriter();
        protected override Text.IndentedWriter TestObject => _obj;
    }
}
