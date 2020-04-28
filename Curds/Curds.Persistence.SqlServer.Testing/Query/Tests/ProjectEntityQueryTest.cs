using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Curds.Persistence.Query.Tests
{
    using Implementation;
    using Persistence.Domain;
    using Model.Domain;
    using Abstraction;
    using Model.Abstraction;

    [TestClass]
    public class ProjectEntityQueryTest
    {
        private Table TestTable = new Table();
        private List<Column> TestColumns = new List<Column>();
        private Column TestColumnOne = new Column();
        private Column TestColumnTwo = new Column();

        private Mock<ISqlQueryWriter> MockQueryWriter = new Mock<ISqlQueryWriter>();
        private Mock<IEntityModel<TestEntity>> MockEntityModel = new Mock<IEntityModel<TestEntity>>();

        private ProjectEntityQuery<TestEntity> TestObject = new ProjectEntityQuery<TestEntity>();

        [TestInitialize]
        public void Init()
        {
            TestColumns.Add(TestColumnOne);
            TestColumns.Add(TestColumnTwo);
            TestTable.Columns = TestColumns;

            MockEntityModel
                .Setup(model => model.Table())
                .Returns(TestTable);

            TestObject.Model = MockEntityModel.Object;
        }

        [TestMethod]
        public void WriteCallsWriterCorrectly()
        {
            int callOrder = 0;
            MockQueryWriter
                .Setup(writer => writer.Select(It.IsAny<List<Column>>()))
                .Callback(() => Assert.AreEqual(callOrder++, 0));
            MockQueryWriter
                .Setup(writer => writer.From(It.IsAny<Table>()))
                .Callback(() => Assert.AreEqual(callOrder++, 1));

            TestObject.Write(MockQueryWriter.Object);

            MockQueryWriter.Verify(writer => writer.Select(TestColumns), Times.Once);
            MockQueryWriter.Verify(writer => writer.From(TestTable), Times.Once);
        }

        [TestMethod]
        public void Read()
        {
            throw new NotImplementedException();
        }

    }
}
