using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Curds.Persistence.Tests
{
    using Domain;
    using Implementation;

    [TestClass]
    public class SqlConnectionStringFactoryTest
    {
        private SqlConnectionInformation TestConnectionInformation = new SqlConnectionInformation();
        private string TestServer = nameof(TestServer);
        private string TestDatabase = nameof(TestDatabase);
        private string TestUserName = nameof(TestUserName);
        private string TestPassword = nameof(TestPassword);

        private SqlConnectionStringFactory TestObject = new SqlConnectionStringFactory();

        [TestInitialize]
        public void Init()
        {
            TestConnectionInformation.Server = TestServer;
            TestConnectionInformation.Database = TestDatabase;
            TestConnectionInformation.UserName = TestUserName;
            TestConnectionInformation.Password = TestPassword;
        }

        [TestMethod]
        public void BuildsFullConnectionWhenUserAndPass()
        {
            string actual = TestObject.Build(TestConnectionInformation);

            Assert.AreEqual(ExpectedFullConnectionString, actual);
        }
        private string ExpectedFullConnectionString => $"Server={TestServer};Database={TestDatabase};User Id={TestUserName};Password={TestPassword}";

        [TestMethod]
        public void BuildsTrustedConnectionWhenNoUserOrPass()
        {
            TestConnectionInformation.UserName = null;
            TestConnectionInformation.Password = null;

            string actual = TestObject.Build(TestConnectionInformation);

            Assert.AreEqual(ExpectedTrustedConnectionString, actual);
        }
        private string ExpectedTrustedConnectionString => $"Server={TestServer};Database={TestDatabase};Trusted_Connection=True;";

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ThrowsWhenNoUser()
        {
            TestConnectionInformation.UserName = null;

            TestObject.Build(TestConnectionInformation);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ThrowsWhenNoPass()
        {
            TestConnectionInformation.Password = null;

            TestObject.Build(TestConnectionInformation);
        }
    }
}
