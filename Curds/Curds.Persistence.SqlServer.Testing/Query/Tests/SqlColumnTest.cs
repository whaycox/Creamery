using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Data;

namespace Curds.Persistence.Query.Tests
{
    using Abstraction;
    using Implementation;
    using Model.Abstraction;

    [TestClass]
    public class SqlColumnTest
    {
        private Mock<ISqlTable> MockTable = new Mock<ISqlTable>();
        private Mock<IValueModel> MockValueModel = new Mock<IValueModel>();

        private SqlColumn TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestObject = new SqlColumn(MockTable.Object, MockValueModel.Object);
        }

        [TestMethod]
        public void TableIsFromConstructor()
        {
            Assert.AreSame(MockTable.Object, TestObject.Table);
        }

        [TestMethod]
        public void NameIsFromValue()
        {
            MockValueModel
                .Setup(model => model.Name)
                .Returns(nameof(NameIsFromValue));

            Assert.AreEqual(nameof(NameIsFromValue), TestObject.Name);
        }

        [TestMethod]
        public void TypeIsFromValue()
        {
            SqlDbType testType = SqlDbType.Binary;
            MockValueModel
                .Setup(model => model.SqlType)
                .Returns(testType);

            Assert.AreEqual(testType, TestObject.Type);
        }

        [TestMethod]
        public void ValueNameIsPropertyName()
        {
            MockValueModel
                .Setup(model => model.Property)
                .Returns(typeof(SqlColumnTest).GetProperty(nameof(TestProperty)));

            Assert.AreEqual(nameof(TestProperty), TestObject.ValueName);
        }
        public string TestProperty => throw new NotImplementedException();
    }
}
