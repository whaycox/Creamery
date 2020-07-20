using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Curds.Persistence.Query.Tokens.Tests
{
    using Implementation;
    using Query.Abstraction;
    using Template;

    [TestClass]
    public class QualifiedObjectSqlQueryTokenTest : BaseSqlQueryTokenTemplate
    {
        private ObjectNameSqlQueryToken TestNameOne = new ObjectNameSqlQueryToken(nameof(TestNameOne));
        private ObjectNameSqlQueryToken TestNameTwo = new ObjectNameSqlQueryToken(nameof(TestNameTwo));
        private string TestSchema = nameof(TestSchema);
        private string TestName = nameof(TestName);
        private string TestColumnName = nameof(TestColumnName);

        private Mock<ISqlTable> MockTable = new Mock<ISqlTable>();
        private Mock<ISqlColumn> MockColumn = new Mock<ISqlColumn>();

        private QualifiedObjectSqlQueryToken TestObject = null;

        [TestInitialize]
        public void Init()
        {
            MockTable
                .Setup(table => table.Schema)
                .Returns(TestSchema);
            MockTable
                .Setup(table => table.Name)
                .Returns(TestName);
            MockColumn
                .Setup(column => column.Table)
                .Returns(MockTable.Object);
            MockColumn
                .Setup(column => column.Name)
                .Returns(TestColumnName);
        }

        [TestMethod]
        public void NamesComeFromConstructor()
        {
            throw new System.NotImplementedException();
            //TestObject = new QualifiedObjectSqlQueryToken(
            //    TestNameOne,
            //    TestNameTwo);

            //Assert.AreEqual(2, TestObject.Names.Count);
            //Assert.AreSame(TestNameOne, TestObject.Names[0]);
            //Assert.AreSame(TestNameTwo, TestObject.Names[1]);
        }

        [TestMethod]
        public void ConstructingWithTableUsesSchemaAndName()
        {
            throw new System.NotImplementedException();
            //TestObject = new QualifiedObjectSqlQueryToken(MockTable.Object);

            //Assert.AreEqual(2, TestObject.Names.Count);
            //Assert.AreEqual(TestSchema, TestObject.Names[0].Name);
            //Assert.AreEqual(TestName, TestObject.Names[1].Name);
        }

        [TestMethod]
        public void ConstructingWithTablePopulatesTable()
        {
            throw new System.NotImplementedException();
            //TestObject = new QualifiedObjectSqlQueryToken(MockTable.Object);

            //Assert.AreSame(MockTable.Object, TestObject.Table);
            //Assert.IsNull(TestObject.Column);
        }

        [TestMethod]
        public void ConstructingWithColumnUsesSchemaAndBothNames()
        {
            throw new System.NotImplementedException();
            //TestObject = new QualifiedObjectSqlQueryToken(MockColumn.Object);

            //Assert.AreEqual(3, TestObject.Names.Count);
            //Assert.AreEqual(TestSchema, TestObject.Names[0].Name);
            //Assert.AreEqual(TestName, TestObject.Names[1].Name);
            //Assert.AreEqual(TestColumnName, TestObject.Names[2].Name);
        }

        [TestMethod]
        public void ConstructingWithColumnPopulatesColumn()
        {
            throw new System.NotImplementedException();
            //TestObject = new QualifiedObjectSqlQueryToken(MockColumn.Object);

            //Assert.AreSame(MockColumn.Object, TestObject.Column);
        }

        [TestMethod]
        public void AcceptFormatVisitorForwardsSingleName()
        {
            throw new System.NotImplementedException();
            //TestObject = new QualifiedObjectSqlQueryToken(TestNameOne);

            //TestObject.AcceptFormatVisitor(MockFormatVisitor.Object);

            //MockFormatVisitor.Verify(visitor => visitor.VisitLiteral(TestNameOne), Times.Once);
        }

        [TestMethod]
        public void AcceptFormatVisitorInterspersesMultipleNamesWithSeparator()
        {
            throw new System.NotImplementedException();
            //TestObject = new QualifiedObjectSqlQueryToken(
            //    TestNameOne,
            //    TestNameTwo);

            //TestObject.AcceptFormatVisitor(MockFormatVisitor.Object);

            //MockFormatVisitor.Verify(visitor => visitor.VisitLiteral(TestNameOne), Times.Once);
            //MockFormatVisitor.Verify(visitor => visitor.VisitLiteral(TestNameTwo), Times.Once);
            //MockFormatVisitor.Verify(visitor => visitor.VisitLiteral(It.Is<LiteralSqlQueryToken>(token => token.Literal == ".")), Times.Once);
        }
    }
}
