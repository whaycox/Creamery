using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using Whey;

namespace Curds.Persistence.Tests
{
    using Abstraction;
    using Implementation;

    [TestClass]
    public class SqlDatabaseTest
    {
        private Mock<ISqlConnectionContext> MockConnectionContext = new Mock<ISqlConnectionContext>();

        private SqlDatabase TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestObject = new SqlDatabase(MockConnectionContext.Object);
        }

        [TestMethod]
        public async Task BeginTransactionInvokesOnConnectionContext()
        {
            await TestObject.BeginTransaction();

            MockConnectionContext.Verify(context => context.BeginTransaction());
        }

        [TestMethod]
        public async Task BeginTransactionReturnsExpectedType()
        {
            IDatabaseTransaction actual = await TestObject.BeginTransaction();

            Assert.IsInstanceOfType(actual, typeof(SqlDatabaseTransaction));
        }

        [TestMethod]
        public async Task BeginTransactionPassesConnectionContext()
        {
            IDatabaseTransaction actual = await TestObject.BeginTransaction();

            SqlDatabaseTransaction actualTransaction = actual.VerifyIsActually<SqlDatabaseTransaction>();
            Assert.AreSame(MockConnectionContext.Object, actualTransaction.ConnectionContext);
        }
    }
}
