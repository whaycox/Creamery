using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using System.Reflection;

namespace Curds.Persistence.Model.Tests
{
    using Implementation;
    using Persistence.Domain;
    using Persistence.Abstraction;
    using Abstraction;
    using Query.Abstraction;

    [TestClass]
    public class ProjectEntityExpressionBuilderTest
    {
        private Type TestEntityType = typeof(OtherEntity);
        private string TestPropertyName = nameof(OtherEntity.ID);
        private PropertyInfo TestIDProperty = null;
        private string TestString = nameof(TestString);
        private bool TestBool = true;
        private byte TestByte = 203;
        private short TestShort = 12345;
        private int TestInt = 15;
        private long TestLong = 100;
        private DateTime TestDateTime = new DateTime(2000, 1, 2, 3, 4, 5);
        private DateTimeOffset TestDateTimeOffset = DateTimeOffset.Now;
        private decimal TestDecimal = 1.234m;
        private double TestDouble = 12.35e5;

        private Mock<ISqlQueryReader> MockQueryReader = new Mock<ISqlQueryReader>();
        private Mock<IEntityModel> MockEntityModel = new Mock<IEntityModel>();
        private Mock<IValueModel> MockValueModel = new Mock<IValueModel>();

        private ProjectEntityExpressionBuilder TestObject = new ProjectEntityExpressionBuilder();

        [TestInitialize]
        public void Init()
        {
            TestIDProperty = TestEntityType.GetProperty(TestPropertyName);

            MockEntityModel
                .Setup(model => model.EntityType)
                .Returns(TestEntityType);
            MockEntityModel
                .Setup(model => model.Values)
                .Returns(new[] { MockValueModel.Object });
            MockValueModel
                .Setup(model => model.Property)
                .Returns(TestIDProperty);
            MockValueModel
                .Setup(model => model.Name)
                .Returns(TestPropertyName);
            MockQueryReader
                .Setup(reader => reader.ReadString(It.IsAny<string>()))
                .Returns(TestString);
            MockQueryReader
                .Setup(reader => reader.ReadBool(It.IsAny<string>()))
                .Returns(TestBool);
            MockQueryReader
                .Setup(reader => reader.ReadByte(It.IsAny<string>()))
                .Returns(TestByte);
            MockQueryReader
                .Setup(reader => reader.ReadShort(It.IsAny<string>()))
                .Returns(TestShort);
            MockQueryReader
                .Setup(reader => reader.ReadInt(It.IsAny<string>()))
                .Returns(TestInt);
            MockQueryReader
                .Setup(reader => reader.ReadLong(It.IsAny<string>()))
                .Returns(TestLong);
            MockQueryReader
                .Setup(reader => reader.ReadDateTime(It.IsAny<string>()))
                .Returns(TestDateTime);
            MockQueryReader
                .Setup(reader => reader.ReadDateTimeOffset(It.IsAny<string>()))
                .Returns(TestDateTimeOffset);
            MockQueryReader
                .Setup(reader => reader.ReadDecimal(It.IsAny<string>()))
                .Returns(TestDecimal);
            MockQueryReader
                .Setup(reader => reader.ReadDouble(It.IsAny<string>()))
                .Returns(TestDouble);
        }

        [TestMethod]
        public void CanBuildDelegate()
        {
            TestObject.BuildProjectEntityDelegate(MockEntityModel.Object);
        }

        [TestMethod]
        public void ColumnReadIsModelName()
        {
            MockValueModel
                .Setup(model => model.Name)
                .Returns(nameof(ColumnReadIsModelName));
            ProjectEntityDelegate projection = TestObject.BuildProjectEntityDelegate(MockEntityModel.Object);

            projection(MockQueryReader.Object);

            MockQueryReader.Verify(reader => reader.ReadInt(nameof(ColumnReadIsModelName)), Times.Once);
        }

        private void VerifyPropertyWasProjected(PropertyInfo testProperty, object expected)
        {
            MockValueModel
                .Setup(model => model.Property)
                .Returns(testProperty);
            ProjectEntityDelegate projection = TestObject.BuildProjectEntityDelegate(MockEntityModel.Object);

            IEntity actual = projection(MockQueryReader.Object);

            Assert.IsInstanceOfType(actual, typeof(OtherEntity));
            OtherEntity actualEntity = (OtherEntity)actual;
            Assert.AreEqual(expected, testProperty.GetMethod.Invoke(actualEntity, null));
        }

        [TestMethod]
        public void CanProjectStringValueFromReader()
        {
            VerifyPropertyWasProjected(TestEntityType.GetProperty(nameof(OtherEntity.Name)), TestString);
            MockQueryReader.Verify(reader => reader.ReadString(TestPropertyName), Times.Once);
        }

        [DataTestMethod]
        [DynamicData(nameof(BooleanValues))]
        public void CanProjectBooleanValueFromReader(PropertyInfo testProperty)
        {
            VerifyPropertyWasProjected(testProperty, TestBool);
            MockQueryReader.Verify(reader => reader.ReadBool(TestPropertyName), Times.Once);
        }
        private static IEnumerable<object[]> BooleanValues => new List<object[]>
        {
            new object[] { typeof(OtherEntity).GetProperty(nameof(OtherEntity.BoolValue)) },
            new object[] { typeof(OtherEntity).GetProperty(nameof(OtherEntity.NullableBoolValue)) },
        };

        [DataTestMethod]
        [DynamicData(nameof(ByteValues))]
        public void CanProjectByteValueFromReader(PropertyInfo testProperty)
        {
            VerifyPropertyWasProjected(testProperty, TestByte);
            MockQueryReader.Verify(reader => reader.ReadByte(TestPropertyName), Times.Once);
        }
        private static IEnumerable<object[]> ByteValues => new List<object[]>
        {
            new object[] { typeof(OtherEntity).GetProperty(nameof(OtherEntity.ByteValue)) },
            new object[] { typeof(OtherEntity).GetProperty(nameof(OtherEntity.NullableByteValue)) },
        };

        [DataTestMethod]
        [DynamicData(nameof(ShortValues))]
        public void CanProjectShortValueFromReader(PropertyInfo testProperty)
        {
            VerifyPropertyWasProjected(testProperty, TestShort);
            MockQueryReader.Verify(reader => reader.ReadShort(TestPropertyName), Times.Once);
        }
        private static IEnumerable<object[]> ShortValues => new List<object[]>
        {
            new object[] { typeof(OtherEntity).GetProperty(nameof(OtherEntity.ShortValue)) },
            new object[] { typeof(OtherEntity).GetProperty(nameof(OtherEntity.NullableShortValue)) },
        };

        [DataTestMethod]
        [DynamicData(nameof(IntegerValues))]
        public void CanProjectIntegerValueFromReader(PropertyInfo testProperty)
        {
            VerifyPropertyWasProjected(testProperty, TestInt);
            MockQueryReader.Verify(reader => reader.ReadInt(TestPropertyName), Times.Once);
        }
        private static IEnumerable<object[]> IntegerValues => new List<object[]>
        {
            new object[] { typeof(OtherEntity).GetProperty(nameof(OtherEntity.IntValue)) },
            new object[] { typeof(OtherEntity).GetProperty(nameof(OtherEntity.NullableIntValue)) },
        };

        [DataTestMethod]
        [DynamicData(nameof(LongValues))]
        public void CanProjectLongValueFromReader(PropertyInfo testProperty)
        {
            VerifyPropertyWasProjected(testProperty, TestLong);
            MockQueryReader.Verify(reader => reader.ReadLong(TestPropertyName), Times.Once);
        }
        private static IEnumerable<object[]> LongValues => new List<object[]>
        {
            new object[] { typeof(OtherEntity).GetProperty(nameof(OtherEntity.LongValue)) },
            new object[] { typeof(OtherEntity).GetProperty(nameof(OtherEntity.NullableLongValue)) },
        };

        [DataTestMethod]
        [DynamicData(nameof(DateTimeValues))]
        public void CanProjectDateTimeValueFromReader(PropertyInfo testProperty)
        {
            VerifyPropertyWasProjected(testProperty, TestDateTime);
            MockQueryReader.Verify(reader => reader.ReadDateTime(TestPropertyName), Times.Once);
        }
        private static IEnumerable<object[]> DateTimeValues => new List<object[]>
        {
            new object[] { typeof(OtherEntity).GetProperty(nameof(OtherEntity.DateTimeValue)) },
            new object[] { typeof(OtherEntity).GetProperty(nameof(OtherEntity.NullableDateTimeValue)) },
        };

        [DataTestMethod]
        [DynamicData(nameof(DateTimeOffsetValues))]
        public void CanProjectDateTimeOffsetValueFromReader(PropertyInfo testProperty)
        {
            VerifyPropertyWasProjected(testProperty, TestDateTimeOffset);
            MockQueryReader.Verify(reader => reader.ReadDateTimeOffset(TestPropertyName), Times.Once);
        }
        private static IEnumerable<object[]> DateTimeOffsetValues => new List<object[]>
        {
            new object[] { typeof(OtherEntity).GetProperty(nameof(OtherEntity.DateTimeOffsetValue)) },
            new object[] { typeof(OtherEntity).GetProperty(nameof(OtherEntity.NullableDateTimeOffsetValue)) },
        };

        [DataTestMethod]
        [DynamicData(nameof(DecimalValues))]
        public void CanProjectDecimalValueFromReader(PropertyInfo testProperty)
        {
            VerifyPropertyWasProjected(testProperty, TestDecimal);
            MockQueryReader.Verify(reader => reader.ReadDecimal(TestPropertyName), Times.Once);
        }
        private static IEnumerable<object[]> DecimalValues => new List<object[]>
        {
            new object[] { typeof(OtherEntity).GetProperty(nameof(OtherEntity.DecimalValue)) },
            new object[] { typeof(OtherEntity).GetProperty(nameof(OtherEntity.NullableDecimalValue)) },
        };

        [DataTestMethod]
        [DynamicData(nameof(DoubleValues))]
        public void CanProjectDoubleValueFromReader(PropertyInfo testProperty)
        {
            VerifyPropertyWasProjected(testProperty, TestDouble);
            MockQueryReader.Verify(reader => reader.ReadDouble(TestPropertyName), Times.Once);
        }
        private static IEnumerable<object[]> DoubleValues => new List<object[]>
        {
            new object[] { typeof(OtherEntity).GetProperty(nameof(OtherEntity.DoubleValue)) },
            new object[] { typeof(OtherEntity).GetProperty(nameof(OtherEntity.NullableDoubleValue)) },
        };
    }
}
