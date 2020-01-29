using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Curds.Persistence.Tests
{
    using Implementation;
    using Abstraction;

    [TestClass]
    public class SqlQueryWriterFactoryTest
    {
        private SqlQueryWriterFactory TestObject = new SqlQueryWriterFactory();

        [TestMethod]
        public void BuildCreatesCorrectType()
        {
            ISqlQueryWriter actual = TestObject.Create();

            Assert.IsInstanceOfType(actual, typeof(SqlQueryWriter));
        }
    }
}
