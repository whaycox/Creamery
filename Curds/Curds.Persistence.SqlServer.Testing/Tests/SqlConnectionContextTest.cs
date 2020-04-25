using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System;

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

        private Mock<IServiceProvider> MockServiceProvider = new Mock<IServiceProvider>();
        private Mock<ISqlQueryWriter> MockQueryWriter = new Mock<ISqlQueryWriter>();
        private Mock<ISqlQueryReaderFactory> MockQueryReaderFactory = new Mock<ISqlQueryReaderFactory>();
        private Mock<ISqlQueryReader> MockQueryReader = new Mock<ISqlQueryReader>();
        private Mock<ISqlQuery> MockQuery = new Mock<ISqlQuery>();
        private Mock<ISqlQuery<TestEntity>> MockReturnQuery = new Mock<ISqlQuery<TestEntity>>();

        private SqlConnectionContext TestObject = null;

        [TestInitialize]
        public void Init()
        {
            MockServiceProvider
                .Setup(provider => provider.GetService(typeof(ISqlQueryWriter)))
                .Returns(MockQueryWriter.Object);
            MockQueryReaderFactory
                .Setup(factory => factory.Create(It.IsAny<SqlCommand>()))
                .ReturnsAsync(MockQueryReader.Object);
            MockQueryWriter
                .Setup(writer => writer.Flush())
                .Returns(TestCommand);

            TestObject = new SqlConnectionContext(
                MockServiceProvider.Object,
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
            MockQuery.Verify(query => query.Write(MockQueryWriter.Object), Times.Once);
            VerifyCommandWasBuiltCommon();

        }
        private void VerifyCommandWasBuiltWithReturnQuery()
        {
            MockReturnQuery.Verify(query => query.Write(MockQueryWriter.Object), Times.Once);
            VerifyCommandWasBuiltCommon();
        }
        private void VerifyCommandWasBuiltCommon()
        {
            MockServiceProvider.Verify(provider => provider.GetService(typeof(ISqlQueryWriter)), Times.Once);
            MockQueryWriter.Verify(writer => writer.Flush(), Times.Once);
            Assert.IsNotNull(TestCommand.Connection);
            Assert.IsNull(TestCommand.Transaction);
        }

        [TestMethod]
        public async Task ExecuteBuildsCommand()
        {
            await TestObject.Execute(MockQuery.Object);

            VerifyCommandWasBuiltWithQuery();
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
