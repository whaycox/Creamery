﻿using System;
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
    using Persistence.Domain;
    using Implementation;
    using Domain;
    using Persistence.Abstraction;

    [TestClass]
    public class InsertQueryTest
    {
        private Table TestTable = new Table();
        private ValueEntity TestEntity = new ValueEntity();

        private Mock<ISqlQueryWriter> MockQueryWriter = new Mock<ISqlQueryWriter>();

        private InsertQuery<TestEntity> TestObject = new InsertQuery<TestEntity>();

        [TestInitialize]
        public void Init()
        {
            TestObject.Table = TestTable;
            TestObject.Entity = TestEntity;
        }

        [TestMethod]
        public void WriteCallsWriterCorrectly()
        {
            int callOrder = 0;
            MockQueryWriter
                .Setup(writer => writer.Insert(It.IsAny<Table>()))
                .Callback(() => Assert.AreEqual(callOrder++, 0));
            MockQueryWriter
                .Setup(writer => writer.ValueEntities(It.IsAny<List<ValueEntity>>()))
                .Callback(() => Assert.AreEqual(callOrder++, 1));

            TestObject.Write(MockQueryWriter.Object);

            MockQueryWriter.Verify(writer => writer.Insert(TestTable), Times.Once);
            MockQueryWriter.Verify(writer => writer.ValueEntities(It.Is<List<ValueEntity>>(arg => arg.Count() == 1 && arg.First() == TestEntity)), Times.Once);
        }
    }
}