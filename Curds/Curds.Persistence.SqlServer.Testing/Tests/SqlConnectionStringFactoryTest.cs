using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Curds.Persistence.Tests
{
    using Implementation;
    using Domain;

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
        public void BuildsTrustedConnectionWhenNoUserOrPass()
        {
            TestConnectionInformation.UserName = null;
            TestConnectionInformation.Password = null;

            string actual = TestObject.Build(TestConnectionInformation);

            Assert.AreEqual(ExpectedTrustedConnectionString, actual);
        }
        private string ExpectedTrustedConnectionString => $"Server={TestServer};Database={TestDatabase};Trusted_Connection=True;";


    }
}
