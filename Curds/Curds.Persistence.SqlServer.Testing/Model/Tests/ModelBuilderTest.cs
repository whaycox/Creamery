using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Data;

namespace Curds.Persistence.Model.Tests
{
    using Abstraction;
    using Configuration.Abstraction;
    using Configuration.Domain;
    using Domain;
    using Implementation;
    using Persistence.Abstraction;
    using Persistence.Domain;

    [TestClass]
    public class ModelBuilderTest
    {
        private List<Type> TestTableTypes = new List<Type>();
        private Type TestEntityType = typeof(TestEntity);
        private Type OtherEntityType = typeof(OtherEntity);
        private PropertyInfo TestEntityIDProperty = typeof(TestEntity).GetProperty(nameof(TestEntity.ID));
        private PropertyInfo TestEntityNameProperty = typeof(TestEntity).GetProperty(nameof(TestEntity.Name));
        private List<PropertyInfo> TestEntityProperties = new List<PropertyInfo>();
        private string TestSchema = nameof(TestSchema);
        private string TestTable = nameof(TestTable);
        private CompiledConfiguration<ITestDataModel> TestCompiledConfiguration = new CompiledConfiguration<ITestDataModel>(typeof(TestEntity));
        private CompiledColumnConfiguration<ITestDataModel> TestCompiledColumnConfiguration = new CompiledColumnConfiguration<ITestDataModel>(nameof(TestEntity.ID));

        private Mock<IModelConfigurationFactory> MockConfigurationFactory = new Mock<IModelConfigurationFactory>();
        private Mock<ITypeMapper> MockTypeMapper = new Mock<ITypeMapper>();
        private Mock<IDelegateMapper> MockDelegateMapper = new Mock<IDelegateMapper>();
        private Mock<ValueEntityDelegate> MockValueEntityDelegate = new Mock<ValueEntityDelegate>();
        private Mock<AssignIdentityDelegate> MockAssignIdentityDelegate = new Mock<AssignIdentityDelegate>();

        private ModelBuilder TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestTableTypes.Add(TestEntityType);
            TestTableTypes.Add(OtherEntityType);
            TestEntityProperties.Add(TestEntityIDProperty);
            TestEntityProperties.Add(TestEntityNameProperty);
            TestCompiledConfiguration.Schema = TestSchema;
            TestCompiledConfiguration.Table = TestTable;

            MockConfigurationFactory
                .Setup(factory => factory.Build<ITestDataModel>(It.IsAny<Type>()))
                .Returns(TestCompiledConfiguration);
            MockTypeMapper
                .Setup(mapper => mapper.EntityTypes<ITestDataModel>())
                .Returns(TestTableTypes);
            MockTypeMapper
                .Setup(mapper => mapper.ValueTypes(It.IsAny<Type>()))
                .Returns(TestEntityProperties);
            MockDelegateMapper
                .Setup(mapper => mapper.MapValueEntityDelegate<ITestDataModel>(It.IsAny<Type>()))
                .Returns(MockValueEntityDelegate.Object);
            MockDelegateMapper
                .Setup(mapper => mapper.MapAssignIdentityDelegate<ITestDataModel>(It.IsAny<Type>()))
                .Returns(MockAssignIdentityDelegate.Object);

            TestObject = new ModelBuilder(
                MockConfigurationFactory.Object,
                MockTypeMapper.Object,
                MockDelegateMapper.Object);
        }

        [TestMethod]
        public void BuildDefaultColumnIsExpected()
        {
            Column actual = TestObject.BuildDefaultColumn(TestEntityIDProperty);

            Assert.AreEqual(TestEntityIDProperty.Name, actual.Name);
            Assert.AreEqual(SqlDbType.Int, actual.SqlType);
            Assert.IsFalse(actual.IsIdentity);
        }

        [DataTestMethod]
        [DataRow(nameof(OtherEntity.Name), SqlDbType.NVarChar)]
        [DataRow(nameof(OtherEntity.BoolValue), SqlDbType.Bit)]
        [DataRow(nameof(OtherEntity.NullableBoolValue), SqlDbType.Bit)]
        [DataRow(nameof(OtherEntity.ByteValue), SqlDbType.TinyInt)]
        [DataRow(nameof(OtherEntity.NullableByteValue), SqlDbType.TinyInt)]
        [DataRow(nameof(OtherEntity.ShortValue), SqlDbType.SmallInt)]
        [DataRow(nameof(OtherEntity.NullableShortValue), SqlDbType.SmallInt)]
        [DataRow(nameof(OtherEntity.IntValue), SqlDbType.Int)]
        [DataRow(nameof(OtherEntity.NullableIntValue), SqlDbType.Int)]
        [DataRow(nameof(OtherEntity.LongValue), SqlDbType.BigInt)]
        [DataRow(nameof(OtherEntity.NullableLongValue), SqlDbType.BigInt)]
        [DataRow(nameof(OtherEntity.DateTimeValue), SqlDbType.DateTime)]
        [DataRow(nameof(OtherEntity.NullableDateTimeValue), SqlDbType.DateTime)]
        [DataRow(nameof(OtherEntity.DateTimeOffsetValue), SqlDbType.DateTimeOffset)]
        [DataRow(nameof(OtherEntity.NullableDateTimeOffsetValue), SqlDbType.DateTimeOffset)]
        [DataRow(nameof(OtherEntity.DecimalValue), SqlDbType.Decimal)]
        [DataRow(nameof(OtherEntity.NullableDecimalValue), SqlDbType.Decimal)]
        [DataRow(nameof(OtherEntity.DoubleValue), SqlDbType.Float)]
        [DataRow(nameof(OtherEntity.NullableDoubleValue), SqlDbType.Float)]
        public void BuildDefaultColumnMapsSqlTypeCorrectly(string propertyName, SqlDbType expectedType)
        {
            PropertyInfo testProperty = typeof(OtherEntity).GetProperty(propertyName);

            Column actual = TestObject.BuildDefaultColumn(testProperty);

            Assert.AreEqual(expectedType, actual.SqlType);
        }

        [TestMethod]
        public void TablesByTypeFetchesTableTypesFromMapper()
        {
            TestObject.TablesByType<ITestDataModel>();

            MockTypeMapper.Verify(mapper => mapper.EntityTypes<ITestDataModel>(), Times.Once);
        }

        [TestMethod]
        public void TablesByTypeFetchesConfigurationForMappedType()
        {
            Dictionary<Type, Table> actual = TestObject.TablesByType<ITestDataModel>();

            Assert.AreEqual(2, actual.Count);
            MockConfigurationFactory.Verify(factory => factory.Build<ITestDataModel>(TestEntityType), Times.Once);
            MockConfigurationFactory.Verify(factory => factory.Build<ITestDataModel>(OtherEntityType), Times.Once);
        }

        [TestMethod]
        public void TablesByTypeBuildsCorrectTable()
        {
            Dictionary<Type, Table> actual = TestObject.TablesByType<ITestDataModel>();

            Table actualTable = actual[TestEntityType];
            Assert.AreEqual(TestSchema, actualTable.Schema);
            Assert.AreEqual(TestTable, actualTable.Name);
        }

        [TestMethod]
        public void TablesByTypeAddsDefaultColumns()
        {
            Dictionary<Type, Table> actual = TestObject.TablesByType<ITestDataModel>();

            Table actualTable = actual[TestEntityType];
            Assert.AreEqual(2, actualTable.Columns.Count);
            Column idColumn = actualTable.Columns[0];
            Assert.AreEqual(TestEntityIDProperty.Name, idColumn.Name);
            Assert.IsFalse(idColumn.IsIdentity);
            Assert.AreEqual(SqlDbType.Int, idColumn.SqlType);
            Column nameColumn = actualTable.Columns[1];
            Assert.AreEqual(TestEntityNameProperty.Name, nameColumn.Name);
            Assert.IsFalse(nameColumn.IsIdentity);
            Assert.AreEqual(SqlDbType.NVarChar, nameColumn.SqlType);
        }

        [TestMethod]
        public void TablesByTypeCanConfigureColumn()
        {
            TestCompiledColumnConfiguration.Name = nameof(TablesByTypeCanConfigureColumn);
            TestCompiledColumnConfiguration.IsIdentity = true;
            TestCompiledConfiguration.Columns.Add(TestEntityIDProperty.Name, TestCompiledColumnConfiguration);

            Dictionary<Type, Table> actual = TestObject.TablesByType<ITestDataModel>();

            Table actualTable = actual[TestEntityType];
            Column idColumn = actualTable.Columns[0];
            Assert.AreEqual(nameof(TablesByTypeCanConfigureColumn), idColumn.Name);
            Assert.AreEqual(true, idColumn.IsIdentity);
        }

        [TestMethod]
        public void ValueEntityDelegatesByTypeFetchesTableTypesFromMapper()
        {
            TestObject.ValueEntityDelegatesByType<ITestDataModel>();

            MockTypeMapper.Verify(mapper => mapper.EntityTypes<ITestDataModel>(), Times.Once);
        }

        [TestMethod]
        public void ValueEntityDelegatesByTypeFetchesFromMapper()
        {
            Dictionary<Type, ValueEntityDelegate> actual = TestObject.ValueEntityDelegatesByType<ITestDataModel>();

            Assert.AreEqual(2, actual.Count);
            MockDelegateMapper.Verify(mapper => mapper.MapValueEntityDelegate<ITestDataModel>(TestEntityType), Times.Once);
            MockDelegateMapper.Verify(mapper => mapper.MapValueEntityDelegate<ITestDataModel>(OtherEntityType), Times.Once);
        }

        [TestMethod]
        public void ValueEntityDelegatesByTypeReturnsMappedDelegate()
        {
            Dictionary<Type, ValueEntityDelegate> actual = TestObject.ValueEntityDelegatesByType<ITestDataModel>();

            Assert.AreSame(MockValueEntityDelegate.Object, actual[TestEntityType]);
        }

        [TestMethod]
        public void AssignIdentityDelegatesByTypeFetchesTableTypesFromMapper()
        {
            TestObject.AssignIdentityDelegatesByType<ITestDataModel>();

            MockTypeMapper.Verify(mapper => mapper.EntityTypes<ITestDataModel>(), Times.Once);
        }

        [TestMethod]
        public void AssignIdentityDelegatesByTypeBuildsConfigurationForEachTableType()
        {
            TestObject.AssignIdentityDelegatesByType<ITestDataModel>();

            MockConfigurationFactory.Verify(factory => factory.Build<ITestDataModel>(TestEntityType), Times.Once);
            MockConfigurationFactory.Verify(factory => factory.Build<ITestDataModel>(OtherEntityType), Times.Once);
        }

        [TestMethod]
        public void AssignIdentityDelegatesByTypeFetchesFromMapper()
        {
            TestCompiledColumnConfiguration.IsIdentity = true;
            TestCompiledConfiguration.Columns.Add(TestEntityIDProperty.Name, TestCompiledColumnConfiguration);

            Dictionary<Type, AssignIdentityDelegate> actual = TestObject.AssignIdentityDelegatesByType<ITestDataModel>();

            Assert.AreEqual(2, actual.Count);
            MockDelegateMapper.Verify(mapper => mapper.MapAssignIdentityDelegate<ITestDataModel>(TestEntityType), Times.Once);
            MockDelegateMapper.Verify(mapper => mapper.MapAssignIdentityDelegate<ITestDataModel>(OtherEntityType), Times.Once);
        }

        [TestMethod]
        public void AssignIdentityDelegatesByTypeFetchesOnlyForTypesWithIdentity()
        {
            TestCompiledColumnConfiguration.IsIdentity = true;
            TestCompiledConfiguration.Columns.Add(TestEntityIDProperty.Name, TestCompiledColumnConfiguration);
            MockConfigurationFactory
                .Setup(factory => factory.Build<ITestDataModel>(OtherEntityType))
                .Returns(new CompiledConfiguration<ITestDataModel>(OtherEntityType));

            Dictionary<Type, AssignIdentityDelegate> actual = TestObject.AssignIdentityDelegatesByType<ITestDataModel>();

            Assert.AreEqual(1, actual.Count);
            MockDelegateMapper.Verify(mapper => mapper.MapAssignIdentityDelegate<ITestDataModel>(TestEntityType), Times.Once);
        }

        [TestMethod]
        public void AssignIdentityDelegatesByTypeReturnsMappedDelegate()
        {
            TestCompiledColumnConfiguration.IsIdentity = true;
            TestCompiledConfiguration.Columns.Add(TestEntityIDProperty.Name, TestCompiledColumnConfiguration);

            Dictionary<Type, AssignIdentityDelegate> actual = TestObject.AssignIdentityDelegatesByType<ITestDataModel>();

            Assert.AreSame(MockAssignIdentityDelegate.Object, actual[TestEntityType]);
        }
    }
}
