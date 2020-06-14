﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace Curds.Persistence.Query.Tokens.Tests
{
    using Implementation;
    using Query.Abstraction;
    using Template;

    [TestClass]
    public class ColumnListSqlQueryTokenTest : BaseSqlQueryTokenTemplate
    {
        private List<ISqlColumn> TestColumns = new List<ISqlColumn>();

        private ColumnListSqlQueryToken TestObject = null;

        [TestMethod]
        public void ColumnsSuppliedByConstructor()
        {
            for (int i = 0; i < 5; i++)
                TestColumns.Add(Mock.Of<ISqlColumn>());
            TestObject = new ColumnListSqlQueryToken(TestColumns);

            CollectionAssert.AreEqual(TestColumns, TestObject.Columns);
        }

        [TestMethod]
        public void VisitsFormatterAsColumnList()
        {
            TestObject = new ColumnListSqlQueryToken(TestColumns);

            TestObject.AcceptFormatVisitor(MockFormatVisitor.Object);

            MockFormatVisitor.Verify(visitor => visitor.VisitColumnList(TestObject), Times.Once);
        }
    }
}