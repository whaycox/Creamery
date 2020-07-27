using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using System.Data;
using Whey;

namespace Curds.Persistence.Query.Tokens.Tests
{
    using Implementation;
    using Template;
    using Query.Abstraction;
    using Query.Domain;

    [TestClass]
    public class SqlDbTypeSqlQueryTokenTest : BaseSqlQueryTokenTemplate
    {
        private SqlDbType TestColumnType = SqlDbType.Int;

        private Mock<ISqlColumn> MockColumn = new Mock<ISqlColumn>();

        private SqlDbTypeSqlQueryToken TestObject = null;

        [TestInitialize]
        public void Init()
        {
            SetupTokenFactoryForMockToken(factory => factory.Phrase(It.IsAny<ISqlQueryToken[]>()));
            MockColumn
                .Setup(column => column.Type)
                .Returns(TestColumnType);

            TestObject = new SqlDbTypeSqlQueryToken(
                MockTokenFactory.Object,
                MockColumn.Object);
        }

        [TestMethod]
        public void AcceptFormatVisitorGeneratesExpectedPhrase()
        {
            SetupTokenFactoryForMockToken(factory => factory.Keyword(It.IsAny<SqlQueryKeyword>()));

            TestObject.AcceptFormatVisitor(MockFormatVisitor.Object);

            MockTokenFactory.Verify(factory => factory.Phrase(
                MockToken.Object,
                MockToken.Object), Times.Exactly(2));
        }

        [TestMethod]
        public void AcceptFormatVisitorPassesToGeneratePhrase()
        {
            TestObject.AcceptFormatVisitor(MockFormatVisitor.Object);

            MockToken.Verify(token => token.AcceptFormatVisitor(MockFormatVisitor.Object), Times.Once);
        }

        [DataTestMethod]
        [DataRow(SqlDbType.Bit, SqlQueryKeyword.BIT)]
        [DataRow(SqlDbType.TinyInt, SqlQueryKeyword.TINYINT)]
        [DataRow(SqlDbType.SmallInt, SqlQueryKeyword.SMALLINT)]
        [DataRow(SqlDbType.Int, SqlQueryKeyword.INT)]
        [DataRow(SqlDbType.BigInt, SqlQueryKeyword.BIGINT)]
        [DataRow(SqlDbType.DateTime, SqlQueryKeyword.DATETIME)]
        [DataRow(SqlDbType.DateTimeOffset, SqlQueryKeyword.DATETIMEOFFSET)]
        [DataRow(SqlDbType.Float, SqlQueryKeyword.FLOAT)]
        public void GeneratesExpectedKeywordTypes(SqlDbType testType, SqlQueryKeyword expectedKeyword)
        {
            MockColumn
                .Setup(column => column.Type)
                .Returns(testType);

            TestObject.AcceptFormatVisitor(MockFormatVisitor.Object);

            MockTokenFactory.Verify(factory => factory.Keyword(expectedKeyword), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void InvalidDbTypeThrowsOnAcceptFormatVisitor()
        {
            MockColumn
                .Setup(column => column.Type)
                .Returns((SqlDbType)99);

            TestObject.AcceptFormatVisitor(MockFormatVisitor.Object);
        }

        [TestMethod]
        public void AcceptFormatVisitorGeneratesNullabilityPhrase()
        {
            ISqlQueryToken notToken = MockTokenFactory.SetupMock(factory => factory.Keyword(SqlQueryKeyword.NOT));
            ISqlQueryToken nullToken = MockTokenFactory.SetupMock(factory => factory.Keyword(SqlQueryKeyword.NULL));

            TestObject.AcceptFormatVisitor(MockFormatVisitor.Object);

            MockTokenFactory.Verify(factory => factory.Phrase(notToken, nullToken), Times.Once);
        }
    }
}
