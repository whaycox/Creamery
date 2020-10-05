using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Whey;

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
        private List<Type> TestEntityTypes = new List<Type>();
        private Type TestEntityType = typeof(TestEntity);
        private Type OtherEntityType = typeof(OtherEntity);
        private PropertyInfo TestEntityIDProperty = typeof(TestEntity).GetProperty(nameof(TestEntity.ID));
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
            TestEntityTypes.Add(TestEntityType);
            TestCompiledConfiguration.Schema = TestSchema;
            TestCompiledConfiguration.Table = TestTable;
            TestCompiledConfiguration.Columns.Add(nameof(TestEntity.ID), TestCompiledColumnConfiguration);

            MockConfigurationFactory
                .Setup(factory => factory.Build<ITestDataModel>(It.IsAny<Type>()))
                .Returns(TestCompiledConfiguration);
            SetupMapperForProperty(TestEntityIDProperty);

            TestObject = new ModelBuilder(
                MockConfigurationFactory.Object,
                MockTypeMapper.Object,
                MockDelegateMapper.Object);
        }

        private void SetupMapperForProperty(PropertyInfo property)
        {
            TestCompiledConfiguration.Keys.Clear();
            TestCompiledConfiguration.Keys.Add(property.Name);

            MockTypeMapper.Reset();
            MockTypeMapper
                .Setup(mapper => mapper.EntityTypes<ITestDataModel>())
                .Returns(TestEntityTypes);
            MockTypeMapper
                .Setup(mapper => mapper.ValueTypes(It.IsAny<Type>()))
                .Returns(new[] { property });
        }

        [TestMethod]
        public void GetsTypesFromMapper()
        {
            TestObject.BuildEntityModels<ITestDataModel>();

            MockTypeMapper.Verify(mapper => mapper.EntityTypes<ITestDataModel>(), Times.Once);
        }

        [TestMethod]
        public void GetsValueTypesFromMapperForEachEntityType()
        {
            TestEntityTypes.Add(OtherEntityType);

            TestObject.BuildEntityModels<ITestDataModel>();

            MockTypeMapper.Verify(mapper => mapper.ValueTypes(TestEntityType), Times.Once);
            MockTypeMapper.Verify(mapper => mapper.ValueTypes(OtherEntityType), Times.Once);
        }

        [TestMethod]
        public void GetsCompiledConfigurationsForEachType()
        {
            TestEntityTypes.Add(OtherEntityType);

            TestObject.BuildEntityModels<ITestDataModel>();

            MockConfigurationFactory.Verify(factory => factory.Build<ITestDataModel>(TestEntityType), Times.Once);
            MockConfigurationFactory.Verify(factory => factory.Build<ITestDataModel>(OtherEntityType), Times.Once);
        }

        private IEntityModel VerifySuppliedModelHasTestName(Type expectedEntityType) => It.Is<IEntityModel>(model =>
            model.Schema == TestSchema &&
            model.Table == TestTable &&
            model.EntityType == expectedEntityType);

        [TestMethod]
        public void MapsValueEntityDelegateForEachType()
        {
            TestEntityTypes.Add(OtherEntityType);

            TestObject.BuildEntityModels<ITestDataModel>();

            MockDelegateMapper.Verify(mapper => mapper.MapValueEntityDelegate(VerifySuppliedModelHasTestName(TestEntityType)), Times.Once);
            MockDelegateMapper.Verify(mapper => mapper.MapValueEntityDelegate(VerifySuppliedModelHasTestName(OtherEntityType)), Times.Once);
        }

        [TestMethod]
        public void MapsAssignIdentityDelegateForEachType()
        {
            TestEntityTypes.Add(OtherEntityType);
            TestCompiledColumnConfiguration.IsIdentity = true;

            TestObject.BuildEntityModels<ITestDataModel>();

            MockDelegateMapper.Verify(mapper => mapper.MapAssignIdentityDelegate(VerifySuppliedModelHasTestName(TestEntityType)), Times.Once);
            MockDelegateMapper.Verify(mapper => mapper.MapAssignIdentityDelegate(VerifySuppliedModelHasTestName(OtherEntityType)), Times.Once);
        }

        [TestMethod]
        public void MapsProjectEntityDelegateForEachType()
        {
            TestEntityTypes.Add(OtherEntityType);

            TestObject.BuildEntityModels<ITestDataModel>();

            MockDelegateMapper.Verify(mapper => mapper.MapProjectEntityDelegate(VerifySuppliedModelHasTestName(TestEntityType)), Times.Once);
            MockDelegateMapper.Verify(mapper => mapper.MapProjectEntityDelegate(VerifySuppliedModelHasTestName(OtherEntityType)), Times.Once);
        }

        [TestMethod]
        public void ReturnedModelIsExpectedType()
        {
            IEnumerable<IEntityModel> actual = TestObject.BuildEntityModels<ITestDataModel>();

            Assert.AreEqual(1, actual.Count());
            IEntityModel actualEntity = actual.First();
            Assert.IsInstanceOfType(actualEntity, typeof(EntityModel));
        }

        [TestMethod]
        public void ReturnedSchemaIsFromConfiguration()
        {
            TestCompiledConfiguration.Schema = nameof(ReturnedSchemaIsFromConfiguration);

            IEnumerable<IEntityModel> actual = TestObject.BuildEntityModels<ITestDataModel>();

            Assert.AreEqual(1, actual.Count());
            IEntityModel actualEntity = actual.First();
            Assert.AreEqual(nameof(ReturnedSchemaIsFromConfiguration), actualEntity.Schema);
        }

        [TestMethod]
        public void ReturnedTableIsFromConfiguration()
        {
            TestCompiledConfiguration.Table = nameof(ReturnedTableIsFromConfiguration);

            IEnumerable<IEntityModel> actual = TestObject.BuildEntityModels<ITestDataModel>();

            Assert.AreEqual(1, actual.Count());
            IEntityModel actualEntity = actual.First();
            Assert.AreEqual(nameof(ReturnedTableIsFromConfiguration), actualEntity.Table);
        }

        [DataTestMethod]
        [DynamicData(nameof(ExpectedColumnTypes))]
        public void BuildsExpectedColumnForProperty(PropertyInfo testProperty, SqlDbType expectedColumnType)
        {
            SetupMapperForProperty(testProperty);

            IEnumerable<IEntityModel> actual = TestObject.BuildEntityModels<ITestDataModel>();

            Assert.AreEqual(1, actual.Count());
            IEntityModel actualEntity = actual.First();
            Assert.AreEqual(1, actualEntity.Values.Count());
            IValueModel actualValue = actualEntity.Values.First();
            Assert.AreEqual(testProperty.Name, actualValue.Name);
            Assert.IsFalse(actualValue.IsIdentity);
            Assert.AreEqual(expectedColumnType, actualValue.SqlType);
        }
        private static IEnumerable<object[]> ExpectedColumnTypes => new List<object[]>
        {
            new object[] { typeof(OtherEntity).GetProperty(nameof(OtherEntity.Name)), SqlDbType.NVarChar },
            new object[] { typeof(OtherEntity).GetProperty(nameof(OtherEntity.BoolValue)), SqlDbType.Bit },
            new object[] { typeof(OtherEntity).GetProperty(nameof(OtherEntity.NullableBoolValue)), SqlDbType.Bit },
            new object[] { typeof(OtherEntity).GetProperty(nameof(OtherEntity.ByteValue)), SqlDbType.TinyInt },
            new object[] { typeof(OtherEntity).GetProperty(nameof(OtherEntity.NullableByteValue)), SqlDbType.TinyInt },
            new object[] { typeof(OtherEntity).GetProperty(nameof(OtherEntity.ShortValue)), SqlDbType.SmallInt },
            new object[] { typeof(OtherEntity).GetProperty(nameof(OtherEntity.NullableShortValue)), SqlDbType.SmallInt },
            new object[] { typeof(OtherEntity).GetProperty(nameof(OtherEntity.IntValue)), SqlDbType.Int },
            new object[] { typeof(OtherEntity).GetProperty(nameof(OtherEntity.NullableIntValue)), SqlDbType.Int },
            new object[] { typeof(OtherEntity).GetProperty(nameof(OtherEntity.LongValue)), SqlDbType.BigInt },
            new object[] { typeof(OtherEntity).GetProperty(nameof(OtherEntity.NullableLongValue)), SqlDbType.BigInt },
            new object[] { typeof(OtherEntity).GetProperty(nameof(OtherEntity.DateTimeValue)), SqlDbType.DateTime },
            new object[] { typeof(OtherEntity).GetProperty(nameof(OtherEntity.NullableDateTimeValue)), SqlDbType.DateTime },
            new object[] { typeof(OtherEntity).GetProperty(nameof(OtherEntity.DateTimeOffsetValue)), SqlDbType.DateTimeOffset },
            new object[] { typeof(OtherEntity).GetProperty(nameof(OtherEntity.NullableDateTimeOffsetValue)), SqlDbType.DateTimeOffset },
            new object[] { typeof(OtherEntity).GetProperty(nameof(OtherEntity.DecimalValue)), SqlDbType.Decimal },
            new object[] { typeof(OtherEntity).GetProperty(nameof(OtherEntity.NullableDecimalValue)), SqlDbType.Decimal },
            new object[] { typeof(OtherEntity).GetProperty(nameof(OtherEntity.DoubleValue)), SqlDbType.Float },
            new object[] { typeof(OtherEntity).GetProperty(nameof(OtherEntity.NullableDoubleValue)), SqlDbType.Float },
        };

        [TestMethod]
        public void CanConfigureIdentityOnColumn()
        {
            TestCompiledColumnConfiguration.IsIdentity = true;
            SetupMapperForProperty(TestEntityIDProperty);

            IEnumerable<IEntityModel> actual = TestObject.BuildEntityModels<ITestDataModel>();

            Assert.AreEqual(1, actual.Count());
            IEntityModel actualEntity = actual.First();
            Assert.AreEqual(1, actualEntity.Values.Count());
            IValueModel actualValue = actualEntity.Values.First();
            Assert.IsTrue(actualValue.IsIdentity);
        }

        [TestMethod]
        public void CanConfigureNameOnColumn()
        {
            TestCompiledColumnConfiguration.Name = nameof(CanConfigureNameOnColumn);
            SetupMapperForProperty(TestEntityIDProperty);

            IEnumerable<IEntityModel> actual = TestObject.BuildEntityModels<ITestDataModel>();

            Assert.AreEqual(1, actual.Count());
            IEntityModel actualEntity = actual.First();
            Assert.AreEqual(1, actualEntity.Values.Count());
            IValueModel actualValue = actualEntity.Values.First();
            Assert.AreEqual(nameof(CanConfigureNameOnColumn), actualValue.Name);
        }

        [DataTestMethod]
        [DataRow(nameof(TestEnumEntity.IntEnum), SqlDbType.Int)]
        [DataRow(nameof(TestEnumEntity.ShortEnum), SqlDbType.SmallInt)]
        public void CanUseEnumsForColumn(string testPropertyName, SqlDbType expectedDbType)
        {
            PropertyInfo enumProperty = typeof(TestEnumEntity).GetProperty(testPropertyName);
            SetupMapperForProperty(enumProperty);

            IEnumerable<IEntityModel> actual = TestObject.BuildEntityModels<ITestDataModel>();

            Assert.AreEqual(1, actual.Count());
            IEntityModel actualEntity = actual.First();
            Assert.AreEqual(1, actualEntity.Values.Count());
            IValueModel actualValue = actualEntity.Values.First();
            Assert.AreEqual(expectedDbType, actualValue.SqlType);
        }

        [TestMethod]
        public void PopulatesKeysFromConfiguration()
        {
            TestCompiledConfiguration.Keys.Add(TestEntityIDProperty.Name);
            SetupMapperForProperty(TestEntityIDProperty);

            IEnumerable<IEntityModel> actual = TestObject.BuildEntityModels<ITestDataModel>();

            Assert.AreEqual(1, actual.Count());
            EntityModel actualEntity = actual.First().VerifyIsActually<EntityModel>();
            Assert.AreEqual(1, actualEntity.KeyDefinition.Count);
            IValueModel key = actualEntity.KeyDefinition.First();
            Assert.AreEqual(TestEntityIDProperty.Name, key.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void KeyForMissingPropertyThrows()
        {
            MockTypeMapper
                .Setup(mapper => mapper.ValueTypes(It.IsAny<Type>()))
                .Returns(new List<PropertyInfo>());

            TestObject.BuildEntityModels<ITestDataModel>();
        }

        [TestMethod]
        [ExpectedException(typeof(ModelException))]
        public void NoKeysThrows()
        {
            TestCompiledConfiguration.Keys.Clear();

            TestObject.BuildEntityModels<ITestDataModel>();
        }
    }
}
