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
    using Domain;
    using Persistence.Template;

    [TestClass]
    public class SqlQueryReaderTest : SqlTemplate
    {
        private SqlConnection TestConnection = null;
        private SqlCommand TestCommand = null;
        private string TestString = nameof(TestString);
        private bool TestBool = true;
        private byte TestByte = 234;
        private short TestShort = 10000;
        private int TestInt = 1234;
        private long TestLong = long.MinValue;
        private DateTime TestDateTime = new DateTime(2000, 5, 6, 13, 20, 40);
        private DateTimeOffset TestDateTimeOffset = DateTimeOffset.UtcNow;
        private decimal TestDecimal = 123456789.987654321m;
        private double TestDouble = 123.456e-15;
        private string NullSql => $"SELECT NULL AS {nameof(NullSql)}";
        private string TestSql => @$"SELECT
     '{TestString}' AS {nameof(TestString)}
    ,CONVERT(BIT, {(TestBool ? "1" : "0")}) AS {nameof(TestBool)}
    ,CONVERT(TINYINT, {TestByte}) AS {nameof(TestByte)}
    ,CONVERT(SMALLINT, {TestShort}) AS {nameof(TestShort)}
    ,{TestInt} AS {nameof(TestInt)}
    ,CONVERT(BIGINT, {TestLong}) AS {nameof(TestLong)}
    ,CONVERT(DATETIME, '{TestDateTime:yyyy-MM-dd HH:mm:ss}') AS {nameof(TestDateTime)}
    ,CONVERT(DATETIMEOFFSET, '{TestDateTimeOffset:O}') AS {nameof(TestDateTimeOffset)}
    ,CONVERT(NUMERIC(18, 9), {TestDecimal}) AS {nameof(TestDecimal)}
    ,CONVERT(FLOAT, {TestDouble}) AS {nameof(TestDouble)}
";

        private SqlQueryReader TestObject = null;

        [TestInitialize]
        public void Init()
        {
            BuildTestObjectForQuery(TestSql);
        }
        private void BuildTestObjectForQuery(string sql)
        {
            TestConnection = new SqlConnection(TestConnectionString);
            TestConnection.Open();
            TestCommand = new SqlCommand(sql, TestConnection);
            TestObject = new SqlQueryReader(TestCommand.ExecuteReader());
        }

        [TestCleanup]
        public void Dispose()
        {
            TestObject?.Dispose();
            TestCommand?.Dispose();
            TestConnection?.Dispose();
        }

        private void SetupNullColumn()
        {
            Dispose();
            BuildTestObjectForQuery(NullSql);
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
        public async Task CanReadString()
        {
            await TestObject.Advance();

            string actual = TestObject.ReadString(nameof(TestString));

            Assert.AreEqual(TestString, actual);
        }

        [TestMethod]
        public async Task CanReadNullString()
        {
            SetupNullColumn();
            await TestObject.Advance();

            string actual = TestObject.ReadString(nameof(NullSql));

            Assert.IsNull(actual);
        }

        [DataTestMethod]
        [DataRow(false)]
        [DataRow(true)]
        public async Task CanReadBool(bool testBool)
        {
            TestBool = testBool;
            Dispose();
            Init();
            await TestObject.Advance();

            bool? actual = TestObject.ReadBool(nameof(TestBool));

            Assert.AreEqual(TestBool, actual);
        }

        [TestMethod]
        public async Task CanReadNullBool()
        {
            SetupNullColumn();
            await TestObject.Advance();

            bool? actual = TestObject.ReadBool(nameof(NullSql));

            Assert.IsNull(actual);
        }

        [TestMethod]
        public async Task CanReadByte()
        {
            await TestObject.Advance();

            byte? actual = TestObject.ReadByte(nameof(TestByte));

            Assert.AreEqual(TestByte, actual);
        }

        [TestMethod]
        public async Task CanReadNullByte()
        {
            SetupNullColumn();
            await TestObject.Advance();

            byte? actual = TestObject.ReadByte(nameof(NullSql));

            Assert.IsNull(actual);
        }

        [TestMethod]
        public async Task CanReadShort()
        {
            await TestObject.Advance();

            short? actual = TestObject.ReadShort(nameof(TestShort));

            Assert.AreEqual(TestShort, actual);
        }

        [TestMethod]
        public async Task CanReadNullShort()
        {
            SetupNullColumn();
            await TestObject.Advance();

            short? actual = TestObject.ReadShort(nameof(NullSql));

            Assert.IsNull(actual);
        }

        [TestMethod]
        public async Task CanReadInt()
        {
            await TestObject.Advance();

            int? actual = TestObject.ReadInt(nameof(TestInt));

            Assert.AreEqual(TestInt, actual);
        }

        [TestMethod]
        public async Task CanReadNullInt()
        {
            SetupNullColumn();
            await TestObject.Advance();

            int? actual = TestObject.ReadInt(nameof(NullSql));

            Assert.IsNull(actual);
        }

        [TestMethod]
        public async Task CanReadLong()
        {
            await TestObject.Advance();

            long? actual = TestObject.ReadLong(nameof(TestLong));

            Assert.AreEqual(TestLong, actual);
        }

        [TestMethod]
        public async Task CanReadNullLong()
        {
            SetupNullColumn();
            await TestObject.Advance();

            long? actual = TestObject.ReadLong(nameof(NullSql));

            Assert.IsNull(actual);
        }

        [TestMethod]
        public async Task CanReadDateTime()
        {
            await TestObject.Advance();

            DateTime? actual = TestObject.ReadDateTime(nameof(TestDateTime));

            Assert.AreEqual(TestDateTime, actual);
        }

        [TestMethod]
        public async Task CanReadNullDateTime()
        {
            SetupNullColumn();
            await TestObject.Advance();

            DateTime? actual = TestObject.ReadDateTime(nameof(NullSql));

            Assert.IsNull(actual);
        }

        [TestMethod]
        public async Task CanReadDateTimeOffset()
        {
            await TestObject.Advance();

            DateTimeOffset? actual = TestObject.ReadDateTimeOffset(nameof(TestDateTimeOffset));

            Assert.AreEqual(TestDateTimeOffset, actual);
        }

        [TestMethod]
        public async Task CanReadNullDateTimeOffset()
        {
            SetupNullColumn();
            await TestObject.Advance();

            DateTimeOffset? actual = TestObject.ReadDateTimeOffset(nameof(NullSql));

            Assert.IsNull(actual);
        }

        [TestMethod]
        public async Task CanReadDecimal()
        {
            await TestObject.Advance();

            decimal? actual = TestObject.ReadDecimal(nameof(TestDecimal));

            Assert.AreEqual(TestDecimal, actual);
        }

        [TestMethod]
        public async Task CanReadNullDecimal()
        {
            SetupNullColumn();
            await TestObject.Advance();

            decimal? actual = TestObject.ReadDecimal(nameof(NullSql));

            Assert.IsNull(actual);
        }

        [TestMethod]
        public async Task CanReadDouble()
        {
            await TestObject.Advance();

            double? actual = TestObject.ReadDouble(nameof(TestDouble));

            Assert.AreEqual(TestDouble, actual);
        }

        [TestMethod]
        public async Task CanReadNullDouble()
        {
            SetupNullColumn();
            await TestObject.Advance();

            double? actual = TestObject.ReadDouble(nameof(NullSql));

            Assert.IsNull(actual);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidColumnNameException))]
        public async Task WillThrowWithInvalidColumnName()
        {
            await TestObject.Advance();

            TestObject.ReadInt(nameof(WillThrowWithInvalidColumnName));
        }
    }
}
