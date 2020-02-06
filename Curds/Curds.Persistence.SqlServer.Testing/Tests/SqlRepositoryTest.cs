﻿using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Curds.Persistence.Tests
{
    using Implementation;
    using Abstraction;
    using Domain;

    [TestClass]
    public class SqlRepositoryTest
    {
        private TestEntity TestEntity = new TestEntity();

        private Mock<ISqlConnectionContext> MockConnectionContext = new Mock<ISqlConnectionContext>();
        private Mock<ISqlQueryBuilder<ITestDataModel>> MockQueryBuilder = new Mock<ISqlQueryBuilder<ITestDataModel>>();
        private Mock<ISqlQuery> MockQuery = new Mock<ISqlQuery>();

        private SqlRepository<ITestDataModel, TestEntity> TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestObject = new SqlRepository<ITestDataModel, TestEntity>(
                MockConnectionContext.Object,
                MockQueryBuilder.Object);
        }

        [TestMethod]
        public async Task InsertBuildsAppropriateQuery()
        {
            await TestObject.Insert(TestEntity);

            MockQueryBuilder.Verify(builder => builder.Insert(model => model.Table<TestEntity>(), TestEntity), Times.Once);
        }

        [TestMethod]
        public async Task InsertExecutesBuiltQuery()
        {
            MockQueryBuilder
                .Setup(builder => builder.Insert(It.IsAny<Expression<Func<ITestDataModel, ITable<TestEntity>>>>(), It.IsAny<TestEntity>()))
                .Returns(MockQuery.Object);

            await TestObject.Insert(TestEntity);

            MockConnectionContext.Verify(context => context.Execute(MockQuery.Object), Times.Once);
        }

    }
}