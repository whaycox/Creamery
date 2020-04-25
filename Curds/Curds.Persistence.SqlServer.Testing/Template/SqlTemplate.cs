using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Curds.Persistence.Template
{
    using Abstraction;
    using Domain;

    public abstract class SqlTemplate
    {
        protected string TestConnectionString = "Server=localhost\\SQLEXPRESS;Database=Testing;Trusted_Connection=True;";
        protected SqlConnectionInformation TestConnectionInformation = new SqlConnectionInformation();
        protected string TestServer = "localhost\\SQLEXPRESS";
        protected string TestDatabase = "Testing";

        internal Mock<ISqlConnectionStringFactory> MockConnectionStringFactory = new Mock<ISqlConnectionStringFactory>();
        protected Mock<IOptions<SqlConnectionInformation>> MockConnectionOptions = new Mock<IOptions<SqlConnectionInformation>>();

        [TestInitialize]
        public void SetupSqlTemplate()
        {
            MockConnectionStringFactory
                .Setup(factory => factory.Build(It.IsAny<SqlConnectionInformation>()))
                .Returns(TestConnectionString);
            MockConnectionOptions
                .Setup(options => options.Value)
                .Returns(TestConnectionInformation);
        }
    }
}
