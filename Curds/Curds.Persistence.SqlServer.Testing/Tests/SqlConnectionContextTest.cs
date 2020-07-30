using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Curds.Persistence.Tests
{
    using Domain;
    using Implementation;
    using Query.Abstraction;
    using Template;

    [TestClass]
    public class SqlConnectionContextTest : SqlTemplate
    {
        private SqlConnectionInformation TestConnectionInfo = new SqlConnectionInformation();
        private SqlCommand TestCommand = new SqlCommand("SELECT 1");
        private SqlCommand TestInsertCommand = new SqlCommand("INSERT dbo.TestEntity VALUES ('Test') SELECT SCOPE_IDENTITY()");

        private Mock<ILogger<SqlConnectionContext>> MockLogger = new Mock<ILogger<SqlConnectionContext>>();
        private Mock<ISqlQueryReaderFactory> MockQueryReaderFactory = new Mock<ISqlQueryReaderFactory>();
        private Mock<ISqlQueryReader> MockQueryReader = new Mock<ISqlQueryReader>();
        private Mock<ISqlQuery> MockQuery = new Mock<ISqlQuery>();

        private SqlConnectionContext TestObject = null;

        private SqlCommand BuildVerificationCommand(decimal expectedID)
        {
            SqlCommand verify = new SqlCommand($"SELECT * FROM dbo.TestEntity y WHERE y.ID = {expectedID}");
            SetConnectionAndTransaction(verify);

            return verify;
        }

        private void SetConnectionAndTransaction(SqlCommand command)
        {
            command.Connection = TestObject.Connection;
            command.Transaction = TestObject.Transaction;
        }

        [TestInitialize]
        public void Init()
        {
            MockQueryReaderFactory
                .Setup(factory => factory.Create(It.IsAny<SqlCommand>()))
                .ReturnsAsync(MockQueryReader.Object);
            MockQuery
                .Setup(query => query.GenerateCommand())
                .Returns(TestCommand);

            TestObject = new SqlConnectionContext(
                MockLogger.Object,
                MockConnectionStringFactory.Object,
                MockConnectionOptions.Object,
                MockQueryReaderFactory.Object);
        }

        [TestCleanup]
        public void Dispose()
        {
            TestObject?.Dispose();
        }

        private void VerifyCommandWasBuiltWithQuery()
        {
            MockQuery.Verify(query => query.GenerateCommand(), Times.Once);
            VerifyCommandWasBuiltCommon();
        }
        private void VerifyCommandWasBuiltCommon()
        {
            Assert.IsNotNull(TestCommand.Connection);
            Assert.IsNull(TestCommand.Transaction);
        }

        [TestMethod]
        public async Task BeginTransactionOpensConnection()
        {
            await TestObject.BeginTransaction();

            Assert.IsNotNull(TestObject.Connection);
            Assert.AreEqual(ConnectionState.Open, TestObject.Connection.State);
        }

        [TestMethod]
        public async Task BeginTransactionSetsTransaction()
        {
            await TestObject.BeginTransaction();

            Assert.IsNotNull(TestObject.Transaction);
        }

        [TestMethod]
        public async Task RollbackTransactionRollsBackCommands()
        {
            await TestObject.BeginTransaction();
            SetConnectionAndTransaction(TestInsertCommand);
            decimal insertedID = (decimal)TestInsertCommand.ExecuteScalar();

            TestObject.RollbackTransaction();

            SqlCommand verificationCommand = BuildVerificationCommand(insertedID);
            Assert.IsNull(verificationCommand.ExecuteScalar());
        }

        [TestMethod]
        public async Task RollbackTransactionNullsTransaction()
        {
            await TestObject.BeginTransaction();

            TestObject.RollbackTransaction();

            Assert.IsNotNull(TestObject.Connection);
            Assert.IsNull(TestObject.Transaction);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RollbackTransactionThrowsWithoutTransaction()
        {
            TestObject.RollbackTransaction();
        }

        [TestMethod]
        public async Task CommitTransactionCommitsCommands()
        {
            await TestObject.BeginTransaction();
            SetConnectionAndTransaction(TestInsertCommand);
            decimal insertedID = (decimal)TestInsertCommand.ExecuteScalar();

            await TestObject.CommitTransaction();

            SqlCommand verificationCommand = BuildVerificationCommand(insertedID);
            Assert.AreEqual((int)insertedID, verificationCommand.ExecuteScalar());
        }

        [TestMethod]
        public async Task CommitTransactionNullsTransaction()
        {
            await TestObject.BeginTransaction();

            await TestObject.CommitTransaction();

            Assert.IsNotNull(TestObject.Connection);
            Assert.IsNull(TestObject.Transaction);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task CommitTransactionThrowsWithoutTransaction()
        {
            await TestObject.CommitTransaction();
        }

        [TestMethod]
        public async Task ExecuteBuildsCommand()
        {
            await TestObject.Execute(MockQuery.Object);

            VerifyCommandWasBuiltWithQuery();
        }

        [TestMethod]
        public async Task ExecuteAfterTransactionSetsTransaction()
        {
            await TestObject.BeginTransaction();
            await TestObject.Execute(MockQuery.Object);

            Assert.IsNotNull(TestCommand.Connection);
            Assert.IsNotNull(TestCommand.Transaction);
        }

        [TestMethod]
        public async Task MultipleExecutesBuildsOnlyOneConnectionString()
        {
            await TestObject.Execute(MockQuery.Object);

            await TestObject.Execute(MockQuery.Object);

            MockConnectionStringFactory.Verify(factory => factory.Build(TestConnectionInformation), Times.Once);
        }

        [TestMethod]
        public async Task ExecuteBuildsReaderFromFactory()
        {
            await TestObject.Execute(MockQuery.Object);

            MockQueryReaderFactory.Verify(factory => factory.Create(TestCommand), Times.Once);
        }

        [TestMethod]
        public async Task ExecutePassesQueryReaderToQuery()
        {
            await TestObject.Execute(MockQuery.Object);

            MockQuery.Verify(query => query.ProcessResult(MockQueryReader.Object), Times.Once);
        }

        [TestMethod]
        public async Task ExecuteDisposesOfReader()
        {
            await TestObject.Execute(MockQuery.Object);

            MockQueryReader.Verify(reader => reader.Dispose(), Times.Once);
        }
    }
}
