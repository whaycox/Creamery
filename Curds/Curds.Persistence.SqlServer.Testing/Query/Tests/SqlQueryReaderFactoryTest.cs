using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Curds.Persistence.Query.Tests
{
    using Abstraction;
    using Implementation;
    using Persistence.Template;

    [TestClass]
    public class SqlQueryReaderFactoryTest : SqlTemplate
    {
        private SqlConnection TestConnection = null;
        private SqlCommand TestCommand = new SqlCommand("SELECT 1");

        private SqlQueryReaderFactory TestObject = new SqlQueryReaderFactory();

        [TestInitialize]
        public void Init()
        {
            TestConnection = new SqlConnection(TestConnectionString);
            TestConnection.Open();
            TestCommand.Connection = TestConnection;
        }

        [TestCleanup]
        public void Dispose()
        {
            TestCommand?.Dispose();
            TestConnection?.Dispose();
        }

        [TestMethod]
        public async Task CreatesExpectedType()
        {
            using (ISqlQueryReader actual = await TestObject.Create(TestCommand))
                Assert.IsInstanceOfType(actual, typeof(SqlQueryReader));
        }
    }
}
