﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

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

        private Mock<IServiceProvider> MockServiceProvider = new Mock<IServiceProvider>();
        private Mock<ISqlQueryReaderFactory> MockQueryReaderFactory = new Mock<ISqlQueryReaderFactory>();
        private Mock<ISqlQueryReader> MockQueryReader = new Mock<ISqlQueryReader>();
        private Mock<ISqlQuery> MockQuery = new Mock<ISqlQuery>();
        private Mock<ISqlQuery<TestEntity>> MockReturnQuery = new Mock<ISqlQuery<TestEntity>>();

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
            throw new NotImplementedException();
            //MockServiceProvider
            //    .Setup(provider => provider.GetService(typeof(ISqlQueryWriter)))
            //    .Returns(MockQueryWriter.Object);
            //MockQueryReaderFactory
            //    .Setup(factory => factory.Create(It.IsAny<SqlCommand>()))
            //    .ReturnsAsync(MockQueryReader.Object);
            //MockQueryWriter
            //    .Setup(writer => writer.Flush())
            //    .Returns(TestCommand);

            //TestObject = new SqlConnectionContext(
            //    MockServiceProvider.Object,
            //    MockConnectionStringFactory.Object,
            //    MockConnectionOptions.Object,
            //    MockQueryReaderFactory.Object);
        }

        [TestCleanup]
        public void Dispose()
        {
            TestObject?.Dispose();
        }

        private void VerifyCommandWasBuiltWithQuery()
        {
            throw new NotImplementedException();
            //MockQuery.Verify(query => query.Write(MockQueryWriter.Object), Times.Once);
            //VerifyCommandWasBuiltCommon();
        }
        private void VerifyCommandWasBuiltWithReturnQuery()
        {
            throw new NotImplementedException();
            //MockReturnQuery.Verify(query => query.Write(MockQueryWriter.Object), Times.Once);
            //VerifyCommandWasBuiltCommon();
        }
        private void VerifyCommandWasBuiltCommon()
        {
            throw new NotImplementedException();
            //MockServiceProvider.Verify(provider => provider.GetService(typeof(ISqlQueryWriter)), Times.Once);
            //MockQueryWriter.Verify(writer => writer.Flush(), Times.Once);
            //Assert.IsNotNull(TestCommand.Connection);
            //Assert.IsNull(TestCommand.Transaction);
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
        public async Task RollbackTransactionThrowsWithoutTransaction()
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
        public async Task ExecuteWithResultBuildsCommand()
        {
            await TestObject.ExecuteWithResult(MockQuery.Object);

            VerifyCommandWasBuiltWithQuery();
        }

        [TestMethod]
        public async Task ExecuteWithResultBuildsReaderFromFactory()
        {
            await TestObject.ExecuteWithResult(MockQuery.Object);

            MockQueryReaderFactory.Verify(factory => factory.Create(TestCommand), Times.Once);
        }

        [TestMethod]
        public async Task ExecuteWithResultPassesQueryReaderToQuery()
        {
            await TestObject.ExecuteWithResult(MockQuery.Object);

            MockQuery.Verify(query => query.ProcessResult(MockQueryReader.Object), Times.Once);
        }

        [TestMethod]
        public async Task ExecuteWithResultDisposesOfReader()
        {
            await TestObject.ExecuteWithResult(MockQuery.Object);

            MockQueryReader.Verify(reader => reader.Dispose(), Times.Once);
        }

        [TestMethod]
        public async Task ExecuteWithResultGenericBuildsCommand()
        {
            await TestObject.ExecuteWithResult(MockReturnQuery.Object);

            VerifyCommandWasBuiltWithReturnQuery();
        }

        [TestMethod]
        public async Task ExecuteWithResultGenericBuildsReaderFromFactory()
        {
            await TestObject.ExecuteWithResult(MockReturnQuery.Object);

            MockQueryReaderFactory.Verify(factory => factory.Create(TestCommand), Times.Once);
        }

        [TestMethod]
        public async Task ExecuteWithResultGenericPassesQueryReaderToQuery()
        {
            await TestObject.ExecuteWithResult(MockReturnQuery.Object);

            MockReturnQuery.Verify(query => query.ProcessResult(MockQueryReader.Object), Times.Once);
        }

        [TestMethod]
        public async Task ExecuteWithResultGenericDisposesOfReader()
        {
            await TestObject.ExecuteWithResult(MockReturnQuery.Object);

            MockQueryReader.Verify(reader => reader.Dispose(), Times.Once);
        }
    }
}
