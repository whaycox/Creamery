using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Curds.Persistence.Tests
{
    using Abstraction;
    using Implementation;

    [TestClass]
    public class SqlDatabaseTransactionTest
    {
        private Mock<ISqlConnectionContext> MockConnectionContext = new Mock<ISqlConnectionContext>();

        private SqlDatabaseTransaction TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestObject = new SqlDatabaseTransaction(MockConnectionContext.Object);
        }

        [TestMethod]
        public async Task CommitForwardsToContext()
        {
            await TestObject.CommitTransaction();

            MockConnectionContext.Verify(context => context.CommitTransaction(), Times.Once);
        }

        [TestMethod]
        public async Task RollbackForwardsToConnection()
        {
            await TestObject.RollbackTransaction();

            MockConnectionContext.Verify(context => context.RollbackTransaction(), Times.Once);
        }

        [TestMethod]
        public void DisposeRollsContextBack()
        {
            TestObject.Dispose();

            MockConnectionContext.Verify(context => context.RollbackTransaction(), Times.Once);
        }

        [TestMethod]
        public void CanDisposeWithNullContext()
        {
            TestObject = new SqlDatabaseTransaction(null);

            TestObject.Dispose();
        }
    }
}
