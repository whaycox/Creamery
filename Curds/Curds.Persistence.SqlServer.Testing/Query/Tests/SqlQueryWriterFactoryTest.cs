using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Persistence.Query.Tests
{
    using Abstraction;
    using Implementation;

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
