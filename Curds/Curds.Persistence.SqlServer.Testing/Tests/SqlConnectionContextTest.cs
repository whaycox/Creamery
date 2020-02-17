using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;

namespace Curds.Persistence.Tests
{
    using Implementation;
    using Abstraction;
    using Domain;
    using Query.Abstraction;
    using Template;
    using Query.Implementation;

    [TestClass]
    public class SqlConnectionContextTest : SqlTemplate
    {
        private SqlConnectionInformation TestConnectionInfo = new SqlConnectionInformation();
        private SqlCommand TestCommand = new SqlCommand("SELECT 1");

        private Mock<ISqlQueryWriterFactory> MockQueryWriterFactory = new Mock<ISqlQueryWriterFactory>();
        private Mock<ISqlQueryWriter> MockQueryWriter = new Mock<ISqlQueryWriter>();
        private Mock<ISqlQuery> MockQuery = new Mock<ISqlQuery>();

        private SqlConnectionContext TestObject = null;

        [TestInitialize]
        public void Init()
        {
            MockQueryWriterFactory
                .Setup(factory => factory.Create())
                .Returns(MockQueryWriter.Object);
            MockQueryWriter
                .Setup(writer => writer.Flush())
                .Returns(TestCommand);

            TestObject = new SqlConnectionContext(
                MockConnectionStringFactory.Object,
                MockConnectionOptions.Object,
                MockQueryWriterFactory.Object);
        }

        [TestCleanup]
        public void Dispose()
        {
            TestObject?.Dispose();
        }

        private void VerifyCommandWasBuilt()
        {
            MockQueryWriterFactory.Verify(factory => factory.Create(), Times.Once);
            MockQuery.Verify(query => query.Write(MockQueryWriter.Object), Times.Once);
            MockQueryWriter.Verify(writer => writer.Flush(), Times.Once);
            Assert.IsNotNull(TestCommand.Connection);
            Assert.IsNull(TestCommand.Transaction);
        }

        [TestMethod]
        public async Task ExecuteBuildsCommand()
        {
            await TestObject.Execute(MockQuery.Object);

            VerifyCommandWasBuilt();
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

            VerifyCommandWasBuilt();
        }

        [TestMethod]
        public async Task ExecuteWithResultReaderIsExpectedType()
        {
            using (ISqlQueryReader actual = await TestObject.ExecuteWithResult(MockQuery.Object))
                Assert.IsInstanceOfType(actual, typeof(SqlQueryReader));
        }

        [TestMethod]
        public async Task ExecuteWithResultCanRead()
        {
            using (ISqlQueryReader reader = await TestObject.ExecuteWithResult(MockQuery.Object))
            {
                Assert.IsTrue(await reader.Advance());
                Assert.AreEqual(1, reader.ReadInt(0));
                Assert.IsFalse(await reader.Advance());
            }
        }

    }
}
