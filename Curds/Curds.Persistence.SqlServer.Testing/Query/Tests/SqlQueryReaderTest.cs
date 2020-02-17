using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Curds.Persistence.Query.Tests
{
    using Abstraction;
    using Implementation;
    using Persistence.Template;

    [TestClass]
    public class SqlQueryReaderTest : SqlTemplate
    {
        private SqlConnection TestConnection = null;
        private SqlCommand TestCommand = null;
        private int TestInt = 1234;
        private string TestSql => @$"SELECT
     {TestInt}
";

        private SqlQueryReader TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestConnection = new SqlConnection(TestConnectionString);
            TestConnection.Open();
            TestCommand = new SqlCommand(TestSql, TestConnection);
            TestObject = new SqlQueryReader(TestCommand.ExecuteReader());
        }

        [TestCleanup]
        public void Dispose()
        {
            TestObject?.Dispose();
            TestCommand?.Dispose();
            TestConnection?.Dispose();
        }

        [TestMethod]
        public async Task AdvanceIsTrueWhenDataExists()
        {
            Assert.IsTrue(await TestObject.Advance());
        }

        [TestMethod]
        public async Task AdvanceIsFalseWhenDataDoesntExists()
        {
            await TestObject.Advance();

            Assert.IsFalse(await TestObject.Advance());
        }

        [TestMethod]
        public async Task CanReadInt()
        {
            await TestObject.Advance();

            int? actual = TestObject.ReadInt(0);

            Assert.AreEqual(TestInt, actual);
        }
    }
}
